// """Study addon namespace."""

// from contextlib import suppress
// from pathlib import Path
// from time import monotonic
// from typing import Any, Dict, List, NamedTuple, Optional, Tuple

// import pydicom
// from box import Box

// from ambra_sdk.exceptions.service import NotFound
// from ambra_sdk.models import Study as StudyModel
// from ambra_sdk.service.ws import WSManager

using System.Collections.Generic;

namespace AmbraSdk.Addon
{
    public class UploadedImageParams // (NamedTuple):
    {
        // """Image object."""

        string study_uid { get; set; }
        string image_uid { get; set; }
        string image_version { get; set; }
        string study_namespace { get; set; }
        object attr { get; set; }
    }

    public class Study
    {
        // """Study addon namespace."""

        private Api _api;

        public Study(Api api)
        {
            // """Init.

            // :param api: base api
            // """
            _api = api;
        }

        public UploadedImageParams upload_dicom(
            Path dicom_path,
            string namespace_id,
            string engine_fqdn = null
        ) //-> UploadedImageParams:
        {
            // """Upload dicom to namespace.

            // :param dicom_path: path to dicom
            // :param namespace_id: uploading to namespace
            // :param engine_fqdn: fqdn (if None gets namespace fqdn)

            // :return: uploaded image params
            // """
            if (engine_fqdn == null)
            {
                _cached_fqdns = _cached_fqdns ?? new Dictionary<string, string>();
                engine_fqdn = _cached_fqdns.get(namespace_id);
                if (engine_fqdn == null)
                {
                    engine_fqdn = self._api
                        .Namespace
                        .engine_fqdn(namespace_id=namespace_id)
                        .get()
                        .engine_fqdn;
                    self._cached_fqdns[namespace_id] = engine_fqdn;
                }
            }

            using(var dicom_file = File.Open(dicom_path, "rb"))
            {
                response = self._api.Storage.Image.upload(
                    engine_fqdn: engine_fqdn,
                    study_namespace: namespace_id,
                    opened_file: dicom_file
                );
                return UploadedImageParams(
                    study_uid: response.study_uid,
                    image_uid: response.image_uid,
                    image_version: response.image_version,
                    study_namespace: response.study_namespace,
                    attr: response.attr
                );
            }
        }
        public (string, IList<UploadedImageParams>) upload(
            Path study_dir,
            string namespace_id
        ) //-> Tuple[str, List[UploadedImageParams]]:
        {
            // """Upload study to namespace.

            // :param study_dir: path to study dir
            // :param namespace_id: uploading to namespace

            // :raises ValueError: Study dir is not directory
            // :return: list of image params
            // """
            if (!study_dir.is_dir())
                throw new ValueError("study_dir is not directory");

            var images_params = new List<UploadedImageParams>();

            var first_dicom = next(study_dir.iterdir(), None);
            if (first_dicom == null)
                throw new ValueError("study_dir is empty");

            ds = pydicom.dcmread(str(first_dicom));
            patient_name = ds.PatientName;
            study_uid = ds.StudyInstanceUID;
            study_time = ds.StudyTime;
            study_date = ds.StudyDate;

            // create new study
            response_data = self._api.Study.add(
                study_uid=study_uid,
                study_date=study_date,
                study_time=study_time,
                patient_name=patient_name,
                storage_namespace=namespace_id,
                phi_namespace=namespace_id,
                thin=null
            ).get();
            var engine_fqdn = response_data.engine_fqdn;
            var uuid = response_data.uuid;

            // upload images
            foreach(var dicom_path in study_dir.iterdir())
            {
                images_params.append(
                    self.upload_dicom(
                        dicom_path,
                        namespace_id,
                        engine_fqdn
                    )
                );
            }
            // then sync data
            // In api.html sync method have not uuid param...
            // So we use this hardcode:
            request = self._api.Study.sync(image_count=1);
            request._request_data["uuid"] = uuid; // NOQA:WPS437
            request.get();

            return (uuid, images_params);
        }

        public Box wait(
            string study_uid,
            string namespace_id,
            int timeout,
            int ws_timeout
        ) //-> Box:
        {
            // """Wait study in namespace.

            // :param study_uid: study_uid
            // :param namespace_id: namespace
            // :param timeout: time for waiting new study
            // :param ws_timeout: time for waiting in socket
            // :raises TimeoutError: if study not ready by timeout
            // :return: Study box object
            // """

            // // prepare ws
            ws_url = "{url}/channel/websocket".format(
                url=self._api._api_url //,  // NOQA:WPS437
            );
            ws_manager = WSManager(ws_url);
            study = None;
            start = monotonic();

            channel_name = "study.{namespace_id}".format(namespace_id: namespace_id);
            sid = self._api.sid;

            using(var ws = ws_manager.channel(sid, channel_name))
            {
                while (true)
                {
                    if (monotonic() - start >= timeout)
                        break;
                    try
                    {                        
                        study = self._api.Study.get(
                            study_uid=study_uid,
                            storage_namespace=namespace_id
                        ).get();
                    }
                    catch (NotFound nf)
                    {
                    }

                    if (study != null && study.phantom == 0)
                        break;

                    try
                    {    
                        ws.wait_for_event(
                            channel_name,
                            sid,
                            "READY",
                            timeout=ws_timeout
                        );
                    }
                    catch (TimeoutError te)
                    {
                    }
                }
            }

            if (study == null)
                throw new TimeoutError();

            return study;
        }

        public Box upload_and_get(
            Path study_dir,
            string namespace_id,
            int timeout = 200,
            int ws_timeout = 5
        ) //-> Box:
        {
            // """Upload study to namespace.

            // :param study_dir: path to study dir
            // :param namespace_id: uploading to namespace
            // :param timeout: time for waiting new study
            // :param ws_timeout: time for waiting in socket
            // :return: Study box object
            // """
            (var uuid, var images_params) = self.upload(
                study_dir,
                namespace_id
            );
            study_uid = images_params[0].study_uid;
            return self.wait(
                study_uid: study_uid,
                namespace_id: namespace_id,
                timeout: timeout,
                ws_timeout: ws_timeout
            );
        }

        public duplicate_and_get(
            string uuid,
            string namespace_id,
            bool include_attachments = false,
            int timeou = 200,
            int ws_timeout = 5
        )// -> Box:
        {
            // """Duplicate study to namespace.

            // :param uuid: study_uuid
            // :param namespace_id: to namespace_id
            // :param include_attachments: include attachments
            // :param timeout: waiting timeout
            // :param ws_timeout: waiting from ws timeout

            // :return: duplicated study
            // """
            include_attachments_int = include_attachments ? 1 : 0;

            from_study_uid = self._api.Study
                .get(uuid=uuid)
                .only(StudyModel.study_uid)
                .get()
                .study_uid;

            self._api.Study.duplicate(
                uuid: uuid,
                namespace_id: namespace_id,
                include_attachments: include_attachments_int
            ).get();

            return self.wait(
                study_uid: from_study_uid,
                namespace_id: namespace_id,
                timeout: timeout,
                ws_timeout: ws_timeout
            );
        }
    }
}
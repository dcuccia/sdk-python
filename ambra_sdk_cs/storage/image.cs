// """Storage image namespace."""

// from io import BufferedReader
// from typing import Optional, Set

// from box import Box

// from ambra_sdk.storage.bool_to_int import bool_to_int
// from ambra_sdk.storage.response import check_response
using System.Collections.Generic;
using System.IO;

namespace AmbraSdk.Storage
{
    public class Image
    {
        // """Storage Image commands."""
        private Storage _storage;
        public Image(Storage storage)
        {
            // """init.

            // :param storage: storage api
            // """
            _storage = storage;
        }
        
        public Box upload(
            string engine_fqdn,
            string study_namespace,
            Stream opened_file //: BufferedReader,
        ) //-> Box:
        {    // """Upload image to a namespace.

            // URL: /namespace/{namespace}/image?sid={sid}

            // :param engine_fqdn: Engine FQDN (Required).
            // :param study_namespace: Namespace (Required).
            // :param opened_file: Opened file (Required).

            // :returns: image object attributes
            // """
            var url_template = $@"/namespace/{study_namespace}/image";
            var url_arg_names = new []{"engine_fqdn", "namespace"};
            var request_arg_names = new List<string>();

            (string url, string request_data) = _storage.get_url_and_request(
                url_template,
                url_arg_names,
                request_arg_names
                //locals(),
            );

            var response = _storage.post(
                url: url,
                parameters: request_data,
                data: opened_file
            );

            var response2 = check_response(response, url_arg_names: url_arg_names);

            return Box(response2.json());
        }

        // TODO: What to do with tags?
        public Box wrap(
            string engine_fqdn,
            string study_namespace,
            Stream opened_file,//: BufferedReader,
            string tags = null,
            bool render_wrapped_pdf = null
        )
        { //-> Box:
            // """Upload a non DICOM image.

            // URL: /namespace/{namespace}/wrap?sid={sid}&render_wrapped_pdf={0,1}

            // :param engine_fqdn: Engine FQDN (Required).
            // :param study_namespace: Namespace (Required).
            // :param tags: Any DICOM tags to be overwrite or added should be provided as a form-data field.
            // :param opened_file: The multipart file to be uploaded should be provided as a form-data field.
            // :param render_wrapped_pdf: An integer value of either 0 or 1.

            // :returns: image object attributes
            // """
            var render_wrapped_pdf = render_wrapped_pdf ? 1 : 0;//  // type: ignore
            var url_template = $@"/namespace/{study_namespace}/wrap";
            var url_arg_names = new []{"engine_fqdn", "namespace"};
            var request_arg_names = new List<string>();
            (var url, var request_data) = _storage.get_url_and_request(
                url_template,
                url_arg_names,
                request_arg_names//,
                //locals(),
            );
            var files = new Dictionary<string, Stream>{
                {"file", opened_file},
            };

            string response = null;
            if(tags != null)
            {
                var post_data = new Dictionary<string, string>{
                    {"tags", tags},
                };
                response = _storage.post(
                    url,
                    parameters: request_data,
                    files: files,
                    data: post_data
                );
            }
            else
            {
                response = self._storage.post(
                    url,
                    parameters: request_data,
                    files: files
                );
            }
            response = check_response(response, url_arg_names: url_arg_names);
            return Box(response.json());
        }

        public Box cadsr(
            string engine_fqdn,
            string study_namespace,
            string study_uid,
            string image_uid,
            string image_version,
            string phi_namespace = null
        ) //-> Box:
        {
            // """Gets graphical annotations according to vendor definitions for CAD SR object.

            // URL: /study/{study_namespace}/{studyUid}/image/{imageUid}/version/{imageVersion}/cadsr?sid={sid}

            // :param engine_fqdn: Engine FQDN (Required).
            // :param study_namespace: Namespace (Required).
            // :param study_uid: Study uid (Required).
            // :param image_uid: Image uid (Required).
            // :param image_version: image version (Required).
            // :param phi_namespace: A string, set to the UUID
            //     of the namespace where the file was attached
            //     if it was attached to a shared instance of the study
            //     outside of the original storage namespace

            // :returns: the vendor-specified graphical \
            //     annotations, empty if not implemented for the vendor or generating device.
            // """
            var url_template = $@"/study/{study_namespace}/{study_uid}/image/{image_uid}/version/{image_version}/cadsr";
            var url_arg_names = new []{
                "engine_fqdn",
                "namespace",
                "study_uid",
                "image_uid",
                "image_version",
            };
            var request_arg_names = {"phi_namespace"};
            (var url, var request_data) = _storage.get_url_and_request(
                url_template,
                url_arg_names,
                request_arg_names
                // locals(),
            );
            response = self._storage.get(url, parameters: request_data);
            response = check_response(response, url_arg_names: url_arg_names);
            return Box(response.json());
        }
    }
}
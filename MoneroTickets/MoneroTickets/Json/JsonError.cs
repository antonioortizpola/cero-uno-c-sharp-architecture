using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MoneroTickets.Json
{
    public class JsonErrors
    {
        public IList<JsonError> Errors { get; }

        public JsonErrors(ModelStateDictionary modelStateDictionary)
        {
            Errors = modelStateDictionary
                .Select(x =>
                    new JsonError(
                        x.Key,
                        x.Value.Errors.Select(y => y.ErrorMessage).ToList()
                    )
                )
                .ToList();
        }
    }

    public class JsonError
    {
        public string Error { get; }
        public IList<string> ErrorMessages { get; }

        public JsonError(string error, IList<string> errorMessages)
        {
            Error = error;
            ErrorMessages = errorMessages;
        }
    }
}
using System.Text.Json.Serialization;

namespace hcode.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {

        }
        public ResponseModel(T data)
        {
            this.Data = data;
        }

        public void SuccessResponse(string message)
        {
            this.Succeeded = true;
            this.Message = message;
        }
        public void ErrorResponse(string[] errors)
        {
            this.Succeeded = false;
            this.Errors = errors;
            this.Message = "There're something wrong";
        }

       // [JsonPropertyName("data")]
        public T Data { get; set; }

        //[JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        //[JsonPropertyName("Errors")]
        public string[] Errors { get; set; }

        //[JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}

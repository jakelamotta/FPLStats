namespace Common.Dtos
{
    public class PythonResultDto<T>: BaseResultDto<T>
    {
        public string ErrorMessage { get; set; }
        public string Output { get; set; }
    } 
}

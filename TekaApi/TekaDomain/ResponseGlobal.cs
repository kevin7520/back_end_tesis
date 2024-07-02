namespace TekaDomain
{
    public class ResponseGlobal<T>
    {
        public string codigo { get; set; }
        public string mensaje { get; set; }
        public T data { get; set; }
    }
}

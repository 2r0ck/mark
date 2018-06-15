namespace DotNetGigs.Models
{
    public class ValResponse<T>
    {
        public T Value {get;set;}

        public ValResponse(T value)
        {
            Value = value;
        }
    }
}
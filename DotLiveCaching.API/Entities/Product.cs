namespace DotLiveCaching.API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }
}

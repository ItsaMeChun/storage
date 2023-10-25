namespace hcode.DTO
{
    public class TypesDTO
    {
        public int Id { get; set; }

        private string Name { get; set; } = string.Empty;
        
        private List<SauceTypeDTO> SauceType;
    }
}

namespace AprendendoNovaVersao.Model
{
    public class Lancamento
    {
        public int Id { get; set; } 
        public string? Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }  
    }
}

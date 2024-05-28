namespace AIS_Cinema.Areas.Admin.ViewModels
{
    public class SessionMovieBinding
    {
        public string DateTimeStr { get; set; }
        public List<ModelIdWithTitle> Movies { get; set; }
        public int SelectedMovieId { get; set; }
    }
}

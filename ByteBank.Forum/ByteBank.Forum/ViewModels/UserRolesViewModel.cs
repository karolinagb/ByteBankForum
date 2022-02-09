namespace ByteBank.Forum.ViewModels
{
    public class UserRolesViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public bool IsSelecionado { get; set; }

        public UserRolesViewModel()
        {
        }

        public UserRolesViewModel(string id, string nome, bool isSelecionado)
        {
            Id = id;
            Nome = nome;
            IsSelecionado = isSelecionado;
        }
    }
}

namespace MustDo.Data {
    public partial class User {
        public User() {
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public virtual ICollection<Note> Notes { get; set; }
    }
}

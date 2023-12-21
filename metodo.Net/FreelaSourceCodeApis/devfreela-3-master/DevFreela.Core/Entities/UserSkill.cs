namespace DevFreela.Core.Entities
{
    public class UserSkill : BaseEntity
    {
        //habilidade pra usuário será um lançamento de muitos para muitos 
        // uma habilidade já estará em vários usuários 
        // um usuário poderá ter várias habilidades
        //devemos criar um relacionamento de muitos para muitos

        public UserSkill(int idUSer, int idSkill)
        {
            IdUser = idUSer;
            IdSkill = idSkill;
        }

        public int IdUser { get; private set; } //uma habilidade para um usuário
        
        public int IdSkill { get; private set; }

    }
}
namespace Recrutify.DataAccess.Models
{
    public class PrimarySkill : BasePrimarySkill, IDataModel
    {
        public string Description { get; set; }

        public string TestLink { get; set; }
    }
}

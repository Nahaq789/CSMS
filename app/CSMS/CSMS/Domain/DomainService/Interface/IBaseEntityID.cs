namespace CSMS.Domain.DomainService.Interface
{
    public interface IBaseEntityID
    {
        public Guid EntityID { get; protected set; }
    }
}

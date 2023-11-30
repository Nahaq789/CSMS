using CSMS.DomainService.Interface;
using CSMS.Models;
using static CSMS.GlobalEnum.GlobalEnum;
using Microsoft.EntityFrameworkCore;

namespace CSMS.DomainService
{
    public class ContractService : IBaseEntityID, IContractService<ContractModel>
    {
        private readonly ApplicationDbContext _context;
        public Guid EntityID { get; set; }

        public ContractService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<ContractModel> GetByID(Guid id)
        {
            var transaction = _context.Database.CurrentTransaction;
            if(transaction != null)
            {
                await transaction.CreateSavepointAsync("GetByID");
            }
            try
            {
                var result = await _context.Contracts.FindAsync(id) ?? throw new NullReferenceException();
                return result;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackToSavepointAsync("GetByID");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                if(ex.Message == "Object reference not set to an instance of an object.")
                {
                    throw new NullReferenceException();
                }
                throw new Exception();
            }
        }
        public async Task<IEnumerable<ContractModel>> GetAll()
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("GetAll");
            }
            try
            {
                var result = await _context.Contracts.ToListAsync();
                if (result == null) { throw new Exception(); }
                return result;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackToSavepointAsync("GetAll");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                throw new Exception();
            }
        }
        public async Task<Guid> Add(ContractModel contract)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("Add");
            }
            try
            {
                await _context.Contracts.AddAsync(contract);
                await _context.SaveChangesAsync();
                return contract.ContractId;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackToSavepointAsync("Add");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                throw new Exception();
            }
        }
        public async Task<UpdateResult> Update(ContractModel contract)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("Update");
            }
            try
            {
                var target = await _context.Contracts.FirstOrDefaultAsync(x => x.ContractId == contract.ContractId);
                if (target == null) { throw new Exception(); }
                ContractModel contractModel = new ContractModel(
                        contract.ContractId,
                        contract.ContractName,
                        contract.ContractCode,
                        contract.CustomerId,
                        contract.Money,
                        contract.TaxMoney,
                        contract.TaxRate
                    );

                _context.Contracts.Entry(target).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                _context.Contracts.Attach(contractModel);
                _context.Contracts.Update(contractModel);
                await _context.SaveChangesAsync();

                return UpdateResult.Success;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackToSavepointAsync("Update");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                return UpdateResult.Failed;
            }
        }
        public async Task<DeleteResult> Delete(ContractModel contractModel)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CreateSavepointAsync("Delete");
            }
            try
            {
                _context.Contracts.Remove(contractModel);
                await _context.SaveChangesAsync();
                return DeleteResult.Success;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackToSavepointAsync("Delete");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
                return DeleteResult.Failed;
            }
        }
    }
}

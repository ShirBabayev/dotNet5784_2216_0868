using BlApi;
using System.Reflection.Emit;
using System.Xml.Linq;
namespace BlImplementation;
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
            (Id:boEngineer.Id,
            Name: boEngineer.Name,
            Email: boEngineer.Email,
            Level: (DO.EngineerExperience)boEngineer.Level,
            Cost:boEngineer.Cost);
        try
        {
            int engId = _dal.Engineer.Create(doEngineer);
            return engId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        try
        { 
            _dal.Engineer.Delete(id); 
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={id} does Not exist", ex);
        }
    }

    public BO.EngineerInTask GetDetailedTaskForEngineer(int engineerId, int taskId)
    {
        return new BO.EngineerInTask()
        {
            EngineerId= engineerId,
            TaskId= taskId
        };
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id= id,
            Name= doEngineer.Name,
            Email= doEngineer.Email,
            Level= (BO.EngineerExperience)doEngineer.Level,
            Cost= doEngineer.Cost
        };

    }

    public IEnumerable<BO.EngineerInList> ReadAll()
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.EngineerInList
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                });
    }

    public void Update(BO.Engineer item)
    {
             DO.Engineer? new_doEngineer = new DO.Engineer
             (Id: item.Id,
             Name: item.Name,
             Email: item.Email,
             Level: (DO.EngineerExperience)item.Level,
             Cost: item.Cost);
        try
        {
            _dal.Engineer.Update(new_doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={item.Id} does Not exist", ex);
        }

    }
}

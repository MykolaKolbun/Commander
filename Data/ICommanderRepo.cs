using Commander.Models;

namespace Commander.Data
{

    // The contract with consumer
    // It should provide standard CRUD inteface
    public interface ICommanderRepo
    {

        bool SaveChanges();  // Needs to invoke this method if there are any data changing operations

        IEnumerable<Command> GetAllCommands();

        Command GetCommandById(int id);

        void CreateCommand(Command cmd);

        void UpdateCommand(Command cmd);
         
    }
    // Next step to create implementation for our case. File MockCommanderRepo
}
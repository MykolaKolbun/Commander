using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boil an Egg", Line = "Boil Water", Platform="Kettle & Pan"},
                new Command{Id=1, HowTo="Cut Bread", Line = "Get a Knife", Platform="Knife & Chopping board"},
                new Command{Id=2, HowTo="Make a cup of tea", Line = "Place teabag in a cup", Platform="Kettle & Cup"}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command {Id=0, HowTo="Boil an Egg", Line = "Boil; Water", Platform="Kettle & Pan"};
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }

    // Here is a fake data without connecting to DB. To use DB needs to create new repository which imlement ICommandRepo as well.
    // Next Step is creating Controller to work with the data. Folder Controllers, file CommandsController
}
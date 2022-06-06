namespace Aislamientos.Models.Commands
{
    public class Connection
    {
       private string conectionString = "Server=HARVINPC\\SQLEXPRESS;Database=tareabd;Trusted_Connection=True;MultipleActiveResultSets=true";

        public Connection()
        {

                
        }

       public string cadena() {
        return conectionString;
        }
    }
}

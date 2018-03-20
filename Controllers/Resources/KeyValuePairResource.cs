namespace Vega_New.Controllers.Resources
{
    public class KeyValuePairResource
    {
        //we created this class from model resource because there were a lot of different classes that had id and name attributes only
        //this cleans up the code and for classes that need additional properties, we can inherit kvpresource
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
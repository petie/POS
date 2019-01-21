namespace Posnet
{
    public class PosnetProperty
    {
        public PosnetProperty()
        {
        }

        public PosnetProperty(string name, string friendlyName)
        {
            Name = name;
            FriendlyName = friendlyName;
        }

        public string Name { get; set; }

        public string FriendlyName { get; set; }
    }
}

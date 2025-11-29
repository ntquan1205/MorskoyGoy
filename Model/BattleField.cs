namespace MorskoyGoy.Model
{
    public class BattleField
    {
        public object Ships { get; set; }
        public bool AddMarker(object move, out object marker)
        {
            marker = null;
            return true;
        }
    }
}
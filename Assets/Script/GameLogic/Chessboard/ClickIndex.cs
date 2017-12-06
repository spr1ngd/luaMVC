
namespace Game
{
	public struct ClickIndex
	{
		public int x;
		public int y;
		public ClickIndex( int x , int y )
		{
			this.x = x;
			this.y = y;
		}

		public override string ToString ()
		{
			return "["+x + "," + y+"]";
		}
	}
}
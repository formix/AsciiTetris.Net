
using System.Collections.Generic;

namespace Formix.Games.Tetris.Display
{
    public class Collision : HashSet<Sprite>
    {
        public override bool Equals(object obj)
        {
            if (!(obj is Collision other))
            {
                return false;
            }

            if (Count != other.Count)
            {
                return false;
            }

            foreach (var sprite in this)
            {
                if (!other.Contains(sprite))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int h = 0;
            foreach (var item in this)
            {
                h ^= item.GetHashCode();
            }
            return h;
        }
    }
}

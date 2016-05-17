using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPlayerWPF
{
    [Serializable]
    class PlayListVideosList
    {

        private List<string> playlistvideos = new List<string>();
        public void Add(string video)
        {
            playlistvideos.Add(video);
        }

        public List<string> ListOfVideos
        {
            get
            {
                return playlistvideos;
            }
        }
    }
}

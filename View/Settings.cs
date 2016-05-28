using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace View
{

    public class Settings
    {
        public enum FIRST_PLAYER { Human, Computer }
        public enum SOUND { Enable, Disable }
        public enum MOVING_SOUND { Bola,Copot,DoubleKlik,Klik,Laser,PageTurn,Pukulan,Senjata }
        public enum ENDING_SOUND { Ding,LoudAlarm,MetalGong,News,TepukTangan }

        [CategoryAttribute("Game"),
        DisplayName("First Player"),
        DescriptionAttribute("Menentukan pemain pertama")]
        public FIRST_PLAYER FirstPlayer
        {
            get;
            set;
        }

        [CategoryAttribute("Game"),
        DisplayName("Sound"),
        DescriptionAttribute("Mengaktifkan suara")]
        public SOUND Sound
        {
            get;
            set;
        }

        [CategoryAttribute("Game"),
        DisplayName("Moving Sound"),
        DescriptionAttribute("Memilih suara bidak berpindah")]
        public MOVING_SOUND MovingSound
        {
            get;
            set;
        }


        [CategoryAttribute("Game"),
        DisplayName("Ending Sound"),
        DescriptionAttribute("Memilih suara permainan berakhir")]
        public ENDING_SOUND EndingSound
        {
            get;
            set;
        }
    }
}

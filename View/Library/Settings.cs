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
        public enum MOVING_SOUND { Bola, Copot, DoubleKlik, Klik, Laser, PageTurn, Pukulan, Senjata }
        public enum ENDING_SOUND { Ding, LoudAlarm, MetalGong, News, TepukTangan }

        public enum METODE { Root, Tree, TreeVirtualLoss }
        public enum EVALUASI { Cumulative, Simple }


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

        [CategoryAttribute("Metode"),
        DisplayName("Metode"),
        DescriptionAttribute("Memilih metode parallel NMCTS yang akan digunakan komputer")]
        public METODE Metode
        {
            get;
            set;
        }
        [CategoryAttribute("Metode"),
        DisplayName("Fungsi Evaluasi"),
        DescriptionAttribute("Memilih fungsi evaluasi pada tahapan selection yang akan digunakan komputer")]
        public EVALUASI Evaluasi
        {
            get;
            set;
        }

        [CategoryAttribute("Metode"),
         DisplayName("Waktu"),
         DescriptionAttribute("Menentukan lamanya waktu melangkah komputer")]
        public int MoveTime
        {
            set;
            get;
        }
        
        [CategoryAttribute("Metode"),
        DisplayName("Jumlah Parallel Task"),
        DescriptionAttribute("Menentukan jumlah parallel task yang akan digunakan komputer")]
        public int ParallelTask
        {
            set;
            get;
        }
    }
}

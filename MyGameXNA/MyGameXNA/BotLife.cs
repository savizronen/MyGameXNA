using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGameXNA
{
  public  class BotLife
    {
        private float Life;

        public BotLife()
        {
            this.Life = 100;  
        }

        public void AddingLife(float Enargy)
         {
             if (this.Life < 100)
             {
                 this.Life += Enargy;
             }
             if (this.Life > 100)
             {
                 this.Life = 100;
             }


         }
        public void DecreaseLife(float Enargy)
         {
             if (this.Life > 0)
             {
                 this.Life -= Enargy;
             }
             if (this.Life < 0)
             {
                 this.Life = 0;
             }
         }




        public float GetBotLifeValue
         {
             get
             {
                 return this.Life;
             }
             set
             {
                 this.Life = value;
             }
         }









    }
}

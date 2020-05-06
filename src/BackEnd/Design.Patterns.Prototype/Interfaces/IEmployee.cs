﻿namespace Design.Patterns.Prototype.Interfaces
{
    public interface IEmployee
    {
        //método utilizado para teste, pois em C# existe a interface 
        //ICloneable que subistituiria o uso do método CloneEmployee
        IEmployee CloneEmployee();
    }
}


using System;
using Godot;

namespace GodotCSToolbox.Extensions
{
    public static class AudioStreamPlayerExtensions
    {
        /// <summary>
        /// connect the finished signal to itself in order to loop the sound. 
        /// You can disconect it with <see cref="StopLoop(AudioStreamPlayer)"/>.
        /// </summary>
        /// <param name="self"></param>
        //public static void Loop(this AudioStreamPlayer self)
        //{
        //    self.Connect("finished", self, "play");
        //}

        /// <summary>
        /// Stop the looping that started with <see cref="Loop(AudioStreamPlayer)"/>.
        /// </summary>
        /// <param name="self"></param>
        public static void StopLoop(this AudioStreamPlayer self)
        {
            //if (self.IsConnected("finished", self, "play"))
            //{
            //    self.Disconnect("finished", self, "play");
            //}
            //self.Disconnect("finished", self, "play");
            
        }
    }
}

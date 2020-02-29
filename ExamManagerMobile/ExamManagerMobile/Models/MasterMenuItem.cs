using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ExamManagerMobile.Models
{
    public class MasterMenuItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Color BackgroundColor { get; set; }
        public Type TargetType { get; set; }

        public MasterMenuItem(string title, string iconSource, Color color, Type target)
        {
            this.Title = title;
            this.IconSource = iconSource;
            this.BackgroundColor = color;
            this.TargetType = target;
        }
    }
}

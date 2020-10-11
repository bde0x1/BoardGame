namespace BoardGame.Domain_Model
{
    public enum Figure { BlueFigure, RedFigure, YellowFigure, GreenFigure }; 

    public interface IFigure 
    {
        Figure FigureColor { get; set; }
    }
}

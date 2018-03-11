<<<<<<< Updated upstream
public abstract class Shape {

	private Color borderColor;
	private Point center;

	public Shape(){

	}

	~Shape(){

	}

	public virtual void Dispose(){

	}

	public abstract void draw();

	public Color getBorderColor(){

		return null;
	}

	public Point getCenter(){

		return null;
	}

	public abstract vector<Point> location();

	/// 
	/// <param name="destination"></param>
	public abstract void move(Point destination);

	/// 
	/// <param name="newVal"></param>
	public void setBorderColor(Color newVal){

	}

	/// 
	/// <param name="newVal"></param>
	public void setCenter(Point newVal){

	}
=======
using System.Collections.Generic;
using System.Drawing;

public abstract class Shape
{

    private Color borderColor;
    private Point center;
    private Pen borderPen;

    protected Pen BorderPen { get => borderPen;}
    protected Point Center { get => center; set => center = value; }

    public Shape()
    {
        borderColor = Color.Red;
        borderPen = new Pen(borderColor);
        Center = new Point(0, 0);
    }

    public abstract void dragOnCreate(Point startPoint, Point secondPoint);

    public Color getBorderColor()
    {
        return borderColor;
    }

    public Point getCenter()
    {
        return Center;
    }

    public abstract List<Point> Location();

    public virtual void move(Point destination)
    {
        this.Center = destination;
    }

    /// 
    /// <param name="newVal"></param>
    public void setBorderColor(Color newVal)
    {
        if (newVal != null)
        {
            if (borderPen != null)
            {
                borderPen.Dispose();
            }
            borderColor = newVal;
            borderPen = new Pen(borderColor);
        }

    }

    public abstract void draw(Graphics formGraphics);
    /// 
    /// <param name="newVal"></param>
    public void setCenter(Point newVal)
    {
        if (newVal != null)
        {
            Center = newVal;
        }
    }
>>>>>>> Stashed changes

}//end Shape

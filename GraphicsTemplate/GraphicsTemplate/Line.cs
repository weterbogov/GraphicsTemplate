public class Line : OneDimensional {

	private Point point;

	public Line(){

	}

	~Line(){

	}

	public override void Dispose(){

	}

	public abstract void draw();

	public Point GetSecondPoint(){

		return null;
	}

	public abstract vector<Point> location();

	/// 
	/// <param name="destination"></param>
	public abstract void move(Point destination);

	/// 
	/// <param name="newVal"></param>
	public void SetSecondPoint(Point newVal){

	}

}//end Line

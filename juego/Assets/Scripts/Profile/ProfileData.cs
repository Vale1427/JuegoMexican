using System.Collections.Generic;

public class  ProfileData{//datos del jugador
    public string filename;
    public string name;
    public bool newGame;

    //player position 
    public float x;
    public float y;

    public int points;
    public List<string> completedLevels;

    public ProfileData(){//constructor
        this.filename = "None.xml";
        this.name = "none";
        this.newGame = false;

        this.y = this.x = 0;
        
        // lista de niveles completados
        this.completedLevels = new List<string>();
    }

    public ProfileData(string name, bool newGame, float x, float y, int points){

        this.filename = name.Replace(" ", "_") + ".xml";
        this.name = name;
        this.newGame = newGame;
        this.x = x;
        this.y = y;
        // lista de niveles completados
        this.completedLevels = new List<string>();

    }

}
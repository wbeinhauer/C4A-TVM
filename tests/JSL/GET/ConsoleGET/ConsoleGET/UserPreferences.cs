using System;

/// <summary>
/// Zusammenfassungsbeschreibung für Class1
/// </summary>
public static class UserPreferences
{
    String fontsize = ""; // may be one of medium or large
    public static String fontsize_medium = "medium";
    public static String fontsize_large = "large";
    
    String contrast = ""; // may be one of black_on_white or yellow_on_black
    public static String contrast_black_on_white = "black_on_white";
    public static String contrast_yellow_on_black = "medium";
    
    //String timeout = ""; // may be one of medium or long 
    //public Static string fontsize_medium = "medium";
    //public Static string fontsize_medium = "medium";
    
    
    
    public static UserPreferences()
	{
		//
		// TODO: Konstruktorlogik hier hinzufügen
		//
	}

    public void setFontsize(String s)
    {
        fontsize = s;
    }

    public String getFontsize()
    {
        return fontsize;
    }

    public void setContrast(String s)
    {
        contrast = s;
    }

    public String getContrast()
    {
        return contrast;
    }



}

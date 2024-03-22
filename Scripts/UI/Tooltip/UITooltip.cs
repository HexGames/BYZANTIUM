using Godot;

public partial class UITooltip : Control
{ 
    // is beeing duplicated... maybe
    private RichTextLabel Title = null;
    private string Title_Original = null;
    private RichTextLabel Row_1 = null;
    private string Row_1_Original = null;
    private RichTextLabel Row_1_Right = null;
    private string Row_1_Right_Original = null;
    private Control Line_1 = null;
    private RichTextLabel Row_2 = null;
    private string Row_2_Original = null;
    private RichTextLabel Row_2_Right = null;
    private string Row_2_Right_Original = null;
    private Control Line_2 = null;
    private RichTextLabel Row_3 = null;
    private string Row_3_Original = null;
    private RichTextLabel Row_3_Right = null;
    private string Row_3_Right_Original = null;

    Game Game;

    private bool IsTargetHovered = false;
    private bool IsTooltipHovered = false;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        Game = GetNode<Game>("/root/Main/Game");

        Title = GetNode<RichTextLabel>("Container/TitleBG/Title");
        Title_Original = Title.Text;

        Row_1 = GetNode<RichTextLabel>("Container/MarginContainer/Rows/Left_1");
        Row_1_Original = Row_1.Text;
        Row_1_Right = GetNode<RichTextLabel>("Container/MarginContainer/Rows/Left_1/Right_1");
        Row_1_Right_Original = Row_1_Right.Text;

        Line_1 = GetNode<Control>("Container/MarginContainer/Rows/Line_1");

        Row_2 = GetNode<RichTextLabel>("Container/MarginContainer/Rows/Left_2");
        Row_2_Original = Row_2.Text;
        Row_2_Right = GetNode<RichTextLabel>("Container/MarginContainer/Rows/Left_2/Right_2");
        Row_2_Right_Original = Row_2_Right.Text;

        Line_2 = GetNode<Control>("Container/MarginContainer/Rows/Line_2");

        Row_3 = GetNode<RichTextLabel>("Container/MarginContainer/Rows/Left_3");
        Row_3_Original = Row_3.Text;
        Row_3_Right = GetNode<RichTextLabel>("Container/MarginContainer/Rows/Left_3/Right_3");
        Row_3_Right_Original = Row_3_Right.Text;

    }

    //public void Refresh(string title, string description)
    //{
    //    Title.Text = Title_Original.Replace("$value", title);
    //
    //    Row_1.Text = Row_1_Original.Replace("$value", title);
    //    Row_1.Visible = true;
    //
    //    Row_1_Right.Visible = false;
    //    Line_1.Visible = false;
    //    Row_2.Visible = false; 
    //    Row_2_Right.Visible = false;
    //    Line_2.Visible = false;
    //    Row_3.Visible = false;
    //    Row_3_Right.Visible = false;
    //}

    //public void Refresh(string title, string right, string left)
    //{
    //    Title.Text = Title_Original.Replace("$value", title);
    //
    //    Row_1.Text = Row_1_Original.Replace("$value", title);
    //    Row_1.Visible = true;
    //
    //    Row_1_Right.Text = Row_1_Right_Original.Replace("$value", title);
    //    Row_1_Right.Visible = true;
    //
    //    Line_1.Visible = false;
    //    Row_2.Visible = false;
    //    Row_2_Right.Visible = false;
    //    Line_2.Visible = false;
    //    Row_3.Visible = false;
    //    Row_3_Right.Visible = false;
    //}

    public void Refresh(string title, string row_1, string row_1_right = "", string row_2 = "", string row_2_right = "", string row_3 = "", string row_3_right = "")
    {
        Title.Text = Title_Original.Replace("$value", title);

        if (row_1 != "")
        {
            Row_1.Text = Row_1_Original.Replace("$value", row_1);
            Row_1.Visible = true;
        }
        else
        {
            Row_1.Visible = false;
        }

        if (row_1_right != "")
        {
            Row_1_Right.Text = Row_1_Right_Original.Replace("$value", row_1_right);
            Row_1_Right.Visible = true;
        }
        else
        {
            Row_1_Right.Visible = false;
        }

        // ---
        if (row_2 != "")
        {
            Line_1.Visible = true;
            Row_2.Text = Row_2_Original.Replace("$value", row_1);
            Row_2.Visible = true;
        }
        else
        {
            Line_1.Visible = false;
            Row_2.Visible = false;
            Row_2_Right.Visible = false;
            Line_2.Visible = false;
            Row_3.Visible = false;
            Row_3_Right.Visible = false;
            return;
        }

        if (row_2_right != "")
        {
            Row_2_Right.Text = Row_2_Right_Original.Replace("$value", row_2_right);
            Row_2_Right.Visible = true;
        }
        else
        {
            Row_2_Right.Visible = false;
        }

        // ---
        if (row_3 != "")
        {
            Line_2.Visible = true;
            Row_3.Text = Row_3_Original.Replace("$value", row_3);
            Row_3.Visible = true;
        }
        else
        {
            Line_2.Visible = false;
            Row_3.Visible = false;
            Row_3_Right.Visible = false;
            return;
        }

        if (row_3_right != "")
        {
            Row_3_Right.Text = Row_3_Right_Original.Replace("$value", row_3_right);
            Row_3_Right.Visible = true;
        }
        else
        {
            Row_3_Right.Visible = false;
        }
    }

    public void OnHoverEnter()
    {
        IsTooltipHovered = true;
    }

    public void OnHoverExit()
    {
        IsTooltipHovered = false;
    }
    public void TargetHoverEnter()
    {
        IsTargetHovered = true;
    }

    public void TargetHoverExit()
    {
        IsTargetHovered = false;
    }
    public override void _Process(double delta)
    {
        Visible = IsTargetHovered || IsTooltipHovered;
    }
}
using Godot;

public class Launcher : Control
{
  private VBoxContainer launcherUI;
  private ColorRect background;
  private Button backButton;
  private Button examplesButton;
  private Button ecosystemButton;
  private Button quitButton;
  private Label loadingLabel;
  private Label fpsLabel;
  private RichTextLabel links;
  private Control drawSpace;

  private Timer loadingTimer;

  public override void _Ready()
  {
    // Base default clear color
    VisualServer.SetDefaultClearColor(Color.Color8(45, 45, 45));

    background = GetNode<ColorRect>("Background");
    launcherUI = GetNode<VBoxContainer>("Margin/VBox");
    backButton = GetNode<Button>("Margin/BackButton");
    examplesButton = GetNode<Button>("Margin/VBox/Margin/Buttons/ExamplesButton");
    ecosystemButton = GetNode<Button>("Margin/VBox/Margin/Buttons/EcosystemButton");
    quitButton = GetNode<Button>("Margin/VBox/Margin/Buttons/QuitButton");
    links = GetNode<RichTextLabel>("Margin/VBox/Margin/Links");
    drawSpace = GetNode<Control>("DrawSpace");
    fpsLabel = GetNode<Label>("Margin/FPS");
    loadingLabel = GetNode<Label>("Margin/Loading");

    examplesButton.Connect("pressed", this, nameof(LoadSceneExplorer));
    ecosystemButton.Connect("pressed", this, nameof(LoadEcosystem));
    quitButton.Connect("pressed", this, nameof(Quit));
    backButton.Connect("pressed", this, nameof(ReloadLauncher));
    links.Connect("meta_clicked", this, nameof(LinkClicked));

    loadingTimer = new Timer();
    loadingTimer.WaitTime = 0.1f;
    loadingTimer.Autostart = false;
    loadingTimer.OneShot = true;
    AddChild(loadingTimer);
    loadingTimer.Connect("timeout", this, nameof(_LoadSceneExplorerInner));

    ToggleBackUI(false);
  }

  private void LoadSceneExplorer()
  {
    loadingLabel.Show();
    ToggleLauncherUI(false);
    loadingTimer.Start();
  }

  private void _LoadSceneExplorerInner()
  {
    var sceneExplorer = (PackedScene)GD.Load("res://chapters/SceneExplorer.tscn");
    drawSpace.AddChild(sceneExplorer.Instance());

    loadingLabel.Hide();
    ToggleBackUI(true);
  }

  private void LoadEcosystem()
  {
    var ecosystem = (PackedScene)GD.Load("res://ecosystem/Ecosystem.tscn");
    drawSpace.AddChild(ecosystem.Instance());

    ToggleLauncherUI(false);
    ToggleBackUI(true);
  }

  private void ToggleLauncherUI(bool state)
  {
    launcherUI.Visible = state;
    background.Visible = state;
  }

  private void ToggleBackUI(bool state)
  {
    backButton.Visible = state;
  }

  private void LinkClicked(object data)
  {
    if (data is string stringData)
    {
      OS.ShellOpen(stringData);
    }
  }

  private void ReloadLauncher()
  {
    GetTree().ReloadCurrentScene();
  }

  private void Quit()
  {
    GetTree().Quit();
  }

  public override void _Process(float delta)
  {
    fpsLabel.Text = "FPS: " + Engine.GetFramesPerSecond();
  }
}

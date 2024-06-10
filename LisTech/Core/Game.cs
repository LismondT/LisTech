using LisTech.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisTech.Core;

static class Game
{
    private static GameModel? _model;
    private static GameView? _view;
    private static GameControler? _controler;

    public static GameModel? Model => _model ?? null;
    public static GameView? View => _view ?? null;
    public static GameControler? Controler => _controler ?? null;

    public static void Init()
    {
        _model = new GameModel();
        _view = new GameView(_model);
        _controler = new GameControler(_model, _view);
    }
}

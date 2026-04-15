/** @author ?????
 *  @version ?????
 *
 *  Your GOAL is to develop this player to try 
 *  to beat the Expert players in MancunaMatch.
 */

public class PlayerMancuna : Player
{
    private Dictionary<IBoard, Outcome> _cacheMax;
    private Dictionary<IBoard, Outcome> _cacheMin;
	private Dictionary<IBoard, ulong> _boardFrequency;
	// private Dictionary<IBoard, double> _boardScores;
	private ulong _maxDepth;
	// private int _otherPlayer;

	public PlayerMancuna(string name, int pos) : base(name, pos)
	{
		_cacheMax = new Dictionary<IBoard, Outcome>();
		_cacheMin = new Dictionary<IBoard, Outcome>();
		_boardFrequency = new Dictionary<IBoard, ulong>();
		// _boardScores = new Dictionary<IBoard, double>();
		// _maxDepth = 11;
		_maxDepth = ulong.MaxValue;
		// _otherPlayer = GetOtherPlayer();
	}

	private enum Outcome
	{
		Invalid,
		Unknown,
		Win,
		Lose,
		Tie
	}

	public override int play(IBoard board)
	{ 
		// Thread.Sleep(1000);
		// _cacheMax.Clear();
		// _cacheMin.Clear();
		_boardFrequency.Clear();
		// _otherPlayer = GetOtherPlayer();

		int forcedPlay = board.forcedPlay();
		if (forcedPlay != -1)
		{
			List<IBoard> children9;
			List<int> play9;
			(children9, play9) = board.children();
			Outcome outcome9 = GetOutcome(children9[0]);
			// Console.WriteLine("Option: " + outcome9.ToString());
			// Console.WriteLine();
			return forcedPlay;
		}

		List<IBoard> children;
		List<int> play;
		(children, play) = board.children();

		Outcome outcome0 = GetOutcome(children[0]);
		if (outcome0 == Outcome.Win) return play[0];

		Outcome outcome1 = GetOutcome(children[1]);
		if (outcome1 == Outcome.Win) return play[1];

		// Console.WriteLine("Option 0: " + outcome0.ToString());
		// Console.WriteLine("Option 1: " + outcome1.ToString());
		// Console.WriteLine();

		if (outcome0 == Outcome.Tie) return play[0];
		if (outcome1 == Outcome.Tie) return play[1];

		if (outcome0 == Outcome.Unknown) return play[0];
		if (outcome1 == Outcome.Unknown) return play[1];

		return play[0];
	}

	private Outcome GetOutcome(IBoard board)
	{
		return GetOutcome(board, 0, true);
	}

	private Outcome GetOutcome(IBoard board, ulong depth, bool isMin)
	{
		if (depth >= _maxDepth) return Outcome.Unknown;

		UpdateFrequency(board);
		if (IsTied(board)) return Outcome.Tie;

		Outcome cacheResult = LoadFromCache(board, isMin);
		if (cacheResult != Outcome.Invalid) return cacheResult;

		Outcome boardOutcome = GetBoardOutcome(board);
		if (boardOutcome != Outcome.Unknown) return boardOutcome;

		List<IBoard> children;
		(children, _) = board.children();

		Outcome outcome0 = GetOutcome(children[0], depth + 1, !isMin);
		SaveToCache(children[0], outcome0, !isMin);

		if (outcome0 == Outcome.Win && !isMin) return Outcome.Win;
		if (outcome0 == Outcome.Lose && isMin) return Outcome.Lose;

		Outcome outcome1 = Outcome.Invalid;
		if (children.Count > 1)
		{
			outcome1 = GetOutcome(children[1], depth + 1, !isMin);
			SaveToCache(children[1], outcome1, !isMin);
		}
		if (outcome1 == Outcome.Win && !isMin) return Outcome.Win;
		if (outcome1 == Outcome.Lose && isMin) return Outcome.Lose;

		// double outcome0Score = children[0].score();
		// double outcome1Score = children[1].score();
		if (outcome0 == Outcome.Tie || outcome1 == Outcome.Tie) return Outcome.Tie;
		if (outcome0 == Outcome.Unknown || outcome1 == Outcome.Unknown) return Outcome.Unknown;

		return isMin ? Outcome.Win : Outcome.Lose;
	}

	private void SaveToCache(IBoard board, Outcome outcome, bool isMin)
	{
		if (isMin && !_cacheMin.ContainsKey(board))
		{
			_cacheMin.Add(board, outcome);
		}
		else if (!isMin && !_cacheMax.ContainsKey(board))
		{
			_cacheMax.Add(board, outcome);
		}
	}

	private Outcome LoadFromCache(IBoard board, bool isMin)
	{
		Outcome result = Outcome.Invalid;

		if (!isMin && _cacheMax.ContainsKey(board))
		{
			result = _cacheMax[board];
		}
		else if (isMin && _cacheMin.ContainsKey(board))
		{
			result = _cacheMin[board];
		}

		return result;
	}

	private void UpdateFrequency(IBoard board)
	{
		if (!_boardFrequency.ContainsKey(board))
		{
			_boardFrequency[board] = 0;
		}
		_boardFrequency[board]++;
	}

	private bool IsTied(IBoard board)
	{
		return _boardFrequency[board] >= 3;
	}

	private Outcome GetBoardOutcome(IBoard board)
	{
		int curr_outcome = board.winner();
		if (curr_outcome == -2) return Outcome.Unknown;
		if (curr_outcome == -1) return Outcome.Tie;
		if (curr_outcome == _position) return Outcome.Win;
		return Outcome.Lose;
	}

	// private int GetOtherPlayer()
	// {
	// 	if (_position == 0) return 1;
	// 	return 0;
	// }
}

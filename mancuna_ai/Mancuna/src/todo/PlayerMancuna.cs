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

	public PlayerMancuna(string name, int pos) : base(name, pos)
	{
		_cacheMax = new Dictionary<IBoard, Outcome>();
		_cacheMin = new Dictionary<IBoard, Outcome>();
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
		Thread.Sleep(1000);
		_cacheMax.Clear();
		_cacheMin.Clear();
		// ulong maxDepth = 11;
		ulong maxDepth = 10000;

		int forcedPlay = board.forcedPlay();
		if (forcedPlay != -1)
		{
			List<IBoard> children9;
			List<int> play9;
			(children9, play9) = board.children();
			Outcome outcome9 = GetOutcome(children9[0], maxDepth);
			Console.WriteLine("Option: " + outcome9.ToString());
			Console.WriteLine();
			return forcedPlay;
		}

		// Console.WriteLine("Playing");

		List<IBoard> children;
		List<int> play;
		(children, play) = board.children();

		Outcome outcome0 = GetOutcome(children[0], maxDepth);
		if (outcome0 == Outcome.Win)
		{
			// Thread.Sleep(1000);
			Console.WriteLine("### Found winning move! ###");
		}
		if (outcome0 == Outcome.Win) return play[0];

		Outcome outcome1 = GetOutcome(children[1], maxDepth);
		if (outcome1 == Outcome.Win)
		{
			// Thread.Sleep(1000);
			Console.WriteLine("### Found winning move! ###");
		}
		if (outcome1 == Outcome.Win) return play[1];

		Console.WriteLine("Option 0: " + outcome0.ToString());
		Console.WriteLine("Option 1: " + outcome1.ToString());
		Console.WriteLine();

		if (outcome0 == Outcome.Tie) return play[0];
		if (outcome1 == Outcome.Tie) return play[1];

		if (outcome0 == Outcome.Unknown) return play[0];
		if (outcome1 == Outcome.Unknown) return play[1];

		return play[0];
	}

	private Outcome GetOutcome(IBoard board, ulong maxDepth)
	{
		return GetOutcome(board, 0, true, maxDepth);
	}

	private Outcome GetOutcome(IBoard board, ulong depth, bool isMin, ulong maxDepth)
	{
		if (depth >= maxDepth) return Outcome.Unknown;
		// Console.WriteLine("---");
		// Console.WriteLine(board);
		// Console.WriteLine("---");

		Outcome cacheResult = LoadFromCache(board, isMin);
		if (cacheResult != Outcome.Invalid) return cacheResult;

		int curr_outcome = board.winner();
		if (curr_outcome == -1) return Outcome.Tie;
		if (curr_outcome == _position) return Outcome.Win;
		if (curr_outcome != -2) return Outcome.Lose;

		List<IBoard> children;
		List<int> play;
		(children, play) = board.children();

		Outcome outcome0 = GetOutcome(children[0], depth + 1, !isMin, maxDepth);
		SaveToCache(children[0], outcome0, !isMin);

		if (outcome0 == Outcome.Win && !isMin) return Outcome.Win;
		if (outcome0 == Outcome.Lose && isMin) return Outcome.Lose;

		Outcome outcome1 = Outcome.Invalid;
		if (children.Count > 1)
		{
			outcome1 = GetOutcome(children[1], depth + 1, !isMin, maxDepth);
			SaveToCache(children[1], outcome1, !isMin);
		}
		if (outcome1 == Outcome.Win && !isMin) return Outcome.Win;
		if (outcome1 == Outcome.Lose && isMin) return Outcome.Lose;

		if (outcome0 == Outcome.Tie || outcome1 == Outcome.Tie) return Outcome.Tie;
		if (outcome0 == Outcome.Unknown || outcome1 == Outcome.Unknown) return Outcome.Unknown;

		return isMin ? Outcome.Win : Outcome.Lose;
	}

	private void SaveToCache(IBoard board, Outcome outcome, bool isMin)
	{
		// if (outcome == Outcome.Unknown)
		// {
		// 	return;
		// }

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
}

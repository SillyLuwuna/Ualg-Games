/** @author Luna Fonseca
 *  @version 202604261900
 *
 *  Your GOAL is to develop this player to try 
 *  to beat the Expert players in MancunaMatch.
 */

public class PlayerMancuna : Player
{
    private Dictionary<IBoard, Outcome> _cache;
	private Dictionary<IBoard, ulong> _boardFrequency;
	private ulong _maxDepth;

	public PlayerMancuna(string name, int pos) : base(name, pos)
	{
		_cache = new Dictionary<IBoard, Outcome>();
		_boardFrequency = new Dictionary<IBoard, ulong>();
		_maxDepth = ulong.MaxValue;
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
		_boardFrequency.Clear();

		int forcedPlay = board.forcedPlay();
		if (forcedPlay != -1) return forcedPlay;

		List<IBoard> children;
		List<int> play;
		(children, play) = board.children();

		Outcome outcome0 = GetOutcome(children[0], 0, true);
		Outcome outcome1 = GetOutcome(children[1], 0, true);

		if (outcome0 == Outcome.Win) return play[0];
		if (outcome1 == Outcome.Win) return play[1];

		if (outcome0 == Outcome.Tie) return play[0];
		if (outcome1 == Outcome.Tie) return play[1];

		if (outcome0 == Outcome.Unknown) return play[0];
		if (outcome1 == Outcome.Unknown) return play[1];

		return play[0];
	}

	private Outcome GetOutcome(IBoard board, ulong depth, bool isMin)
	{
		if (depth >= _maxDepth) return Outcome.Unknown;

		Outcome cacheResult = LoadFromCache(board, isMin);
		if (cacheResult != Outcome.Invalid) return cacheResult;

		UpdateFrequency(board);
		if (IsTied(board)) return Outcome.Tie;

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

		if (outcome0 == Outcome.Tie || outcome1 == Outcome.Tie) return Outcome.Tie;
		if (outcome0 == Outcome.Unknown || outcome1 == Outcome.Unknown) return Outcome.Unknown;

		return isMin ? Outcome.Win : Outcome.Lose;
	}

	private void SaveToCache(IBoard board, Outcome outcome, bool isMin)
	{
		if (isMin) outcome = ReverseOutcome(outcome);
		_cache.TryAdd(board, outcome);
	}

	private Outcome LoadFromCache(IBoard board, bool isMin)
	{
		Outcome result = Outcome.Invalid;
		bool cacheHit = _cache.TryGetValue(board, out result);
		if (!cacheHit) return Outcome.Invalid;

		if (isMin) result = ReverseOutcome(result);
		return result;
	}

	private void UpdateFrequency(IBoard board)
	{
		_boardFrequency.TryAdd(board, 0);
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

	private Outcome ReverseOutcome(Outcome outcome)
	{
		if (outcome == Outcome.Win) return Outcome.Lose;
		if (outcome == Outcome.Lose) return Outcome.Win;
		return outcome;
	}
}

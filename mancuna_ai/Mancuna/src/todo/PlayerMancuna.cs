/** @author Luna Fonseca
 *  @version 202604181900
 *
 *  Your GOAL is to develop this player to try 
 *  to beat the Expert players in MancunaMatch.
 */

public class PlayerMancuna : Player
{
    private Dictionary<IBoard, Outcome> _cache;
	private Dictionary<IBoard, ulong> _boardFrequency;
	private readonly ulong _maxDepth;
	private bool _first;
	private Random _rng;
	private List<IBoard> _opponentPossiblePlays;
	private int _count;
	private int _cachedPos;

	public PlayerMancuna(string name, int pos) : base(name, pos)
	{
		_cache = new Dictionary<IBoard, Outcome>();
		_boardFrequency = new Dictionary<IBoard, ulong>();
		_maxDepth = ulong.MaxValue;
		_first = true;
		_rng = new Random();
		_opponentPossiblePlays = new List<IBoard>();
		_count = 0;
		_cachedPos = _position; // position isn't accurate unless match.reset() is called which is not the case for human matches
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
		if (_first) CachePlays(board);
		if (!_opponentPossiblePlays.Contains(board)) Reset();

		IncreaseFrequency(board);

		int forcedPlay = board.forcedPlay();
		if (forcedPlay != -1)
		{
			List<IBoard> forcedPlayChildren;
			(forcedPlayChildren, _) = board.children();
			(_opponentPossiblePlays, _) = forcedPlayChildren[0].children();
			IncreaseFrequency(forcedPlayChildren[0]);
			return forcedPlay;
		}

		List<IBoard> children;
		List<int> plays;
		(children, plays) = board.children();

		int decision = Decide(children[0], children[1], plays);
		if (decision == plays[0])
		{
			IncreaseFrequency(children[0]);
			(_opponentPossiblePlays, _) = children[0].children();
		}
		else
		{
			IncreaseFrequency(children[1]);
			(_opponentPossiblePlays, _) = children[1].children();
		}
		return decision;
	}

	private void CachePlays(IBoard board)
	{
		_first = false;
		_cachedPos = _position;
		GetOutcome(board, 0, false);
	}

	private void Reset()
	{
		_boardFrequency.Clear();
		_count++;
		// Console.WriteLine(_count);
	}

	private int Decide(IBoard board0, IBoard board1, List<int> plays)
	{
		Outcome outcome0 = LoadFromCache(board0);
		Outcome outcome1 = LoadFromCache(board1);

		if (outcome0 == Outcome.Win) return plays[0];
		if (outcome1 == Outcome.Win) return plays[1];

		if (outcome0 == Outcome.Tie && outcome1 == Outcome.Tie)
		{
			// double score0 = GetScore(board0, 0, true);
			// double score1 = GetScore(board1, 0, true);
			// return score0 >= score1 ? plays[0] : plays[1];

			// return _rng.Next() <= Int32.MaxValue / 2 ? plays[0] : plays[1];
		}

		if (outcome0 == Outcome.Tie) return plays[0];
		if (outcome1 == Outcome.Tie) return plays[1];

		if (outcome0 == Outcome.Unknown) return plays[0];
		if (outcome1 == Outcome.Unknown) return plays[1];

		return plays[0];
	}

	private Outcome GetOutcome(IBoard board, ulong depth, bool isMin)
	{
		if (depth >= _maxDepth) return Outcome.Unknown;

		Outcome cacheResult = LoadFromCache(board);
		if (cacheResult != Outcome.Invalid) return cacheResult;

		IncreaseFrequency(board);
		if (IsTied(board))
		{
			DecreaseFrequency(board);
			return Outcome.Tie;
		}

		Outcome boardOutcome = GetBoardOutcome(board);
		if (boardOutcome != Outcome.Unknown)
		{
			DecreaseFrequency(board);
			return boardOutcome;
		}

		List<IBoard> children;
		(children, _) = board.children();

		Outcome outcome0 = GetOutcome(children[0], depth + 1, !isMin);
		SaveToCache(children[0], outcome0);

		if (outcome0 == Outcome.Win && !isMin)
		{
			DecreaseFrequency(board);
			return Outcome.Win;
		}
		if (outcome0 == Outcome.Lose && isMin)
		{
			DecreaseFrequency(board);
			return Outcome.Lose;
		}

		Outcome outcome1 = Outcome.Invalid;
		if (children.Count > 1)
		{
			outcome1 = GetOutcome(children[1], depth + 1, !isMin);
			SaveToCache(children[1], outcome1);
		}

		DecreaseFrequency(board);

		if (outcome1 == Outcome.Win && !isMin) return Outcome.Win;
		if (outcome1 == Outcome.Lose && isMin) return Outcome.Lose;

		if (outcome0 == Outcome.Tie || outcome1 == Outcome.Tie) return Outcome.Tie;
		if (outcome0 == Outcome.Unknown || outcome1 == Outcome.Unknown) return Outcome.Unknown;

		return isMin ? Outcome.Win : Outcome.Lose;
	}

	private void SaveToCache(IBoard board, Outcome outcome)
	{
		_cache.TryAdd(new BoardMancuna((BoardMancuna)board), outcome);
	}

	private Outcome LoadFromCache(IBoard board)
	{
		Outcome result = Outcome.Invalid;
		bool cacheHit = _cache.TryGetValue(board, out result);
		if (!cacheHit) return Outcome.Invalid;

		return (_cachedPos != _position) ? ReverseOutcome(result) : result;
	}

	private void IncreaseFrequency(IBoard board)
	{
		_boardFrequency.TryAdd(new BoardMancuna((BoardMancuna)board), 0); // boards aren't immutable! This causes a ton of issues with colliding hashes. Patch fix.
		_boardFrequency[board]++;
	}

	private void DecreaseFrequency(IBoard board)
	{
		_boardFrequency[board]--;
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

	private double GetScore(IBoard board, ulong depth, bool isMin)
	{
		if (depth >= _maxDepth) return -1.0;

		IncreaseFrequency(board);
		if (IsTied(board))
		{
			DecreaseFrequency(board);
			return board.score(_position);
		}

		List<IBoard> children;
		(children, _) = board.children();

		if (children.Count < 2)
		{
			double score = GetScore(children[0], depth + 1, !isMin);
			DecreaseFrequency(board);
			return score;
		}

		Outcome outcome0 = LoadFromCache(children[0]);
		Outcome outcome1 = LoadFromCache(children[1]);

		if (outcome0 == Outcome.Tie && outcome1 == Outcome.Tie)
		{
			double score0 = GetScore(children[0], depth + 1, !isMin);
			double score1 = GetScore(children[1], depth + 1, !isMin);
			DecreaseFrequency(board);
			return isMin ? double.Min(score0, score1) : double.Max(score0, score1);
		}

		if (outcome0 == Outcome.Tie)
		{
			double score = GetScore(children[0], depth + 1, !isMin);
			DecreaseFrequency(board);
			return score;
		}
		else if (outcome1 == Outcome.Tie)
		{
			double score = GetScore(children[1], depth + 1, !isMin);
			DecreaseFrequency(board);
			return score;
		}

		DecreaseFrequency(board);
		// return -1.0;
		throw new Exception("Invalid state.");
	}

	private bool IsGameOver(IBoard board)
	{
		return IsTied(board) || board.winner() != -2;
	}
}

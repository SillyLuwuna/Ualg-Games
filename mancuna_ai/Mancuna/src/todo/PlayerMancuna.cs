/** @author ?????
 *  @version ?????
 *
 *  Your GOAL is to develop this player to try 
 *  to beat the Expert players in MancunaMatch.
 */

public class PlayerMancuna : Player
{
    // private Dictionary<IBoard, double> _cache;
    // private Dictionary<IBoard, double> _cacheTied;
	private List<Dictionary<IBoard, double>> _cache;
	private Dictionary<IBoard, int> _boardFrequency;
	private ulong _maxDepth;
	private int _firstBoardHash;
	private int _count;

	public PlayerMancuna(string name, int pos) : base(name, pos)
	{
		// _cache = new Dictionary<IBoard, double>();
		_cache = new List<Dictionary<IBoard, double>>();
		_boardFrequency = new Dictionary<IBoard, int>();
		// _maxDepth = ulong.MaxValue;
		_maxDepth = 3;
		_firstBoardHash = -1;
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
		if (HasReset(board))
		{
			_count++;
			Console.WriteLine(_count);
			_boardFrequency.Clear();
		}
		// Thread.Sleep(1000);
		IncreaseFrequency(board);

		int forcedPlay = board.forcedPlay();
		if (forcedPlay != -1)
		{
			List<IBoard> fchildren;
			(fchildren, _) = board.children();
			IncreaseFrequency(fchildren[0]);
			return forcedPlay;
		}

		List<IBoard> children;
		List<int> play;
		(children, play) = board.children();
		double score0 = MinMax(children[0], 0, true);
		double score1 = MinMax(children[1], 0, true);
		// Console.WriteLine("Option 0: " + score0);
		// Console.WriteLine("Option 1: " + score1);
		// Console.WriteLine();

		if (score0 > score1)
		{
			IncreaseFrequency(children[0]);
			return play[0];
		}
		IncreaseFrequency(children[1]);
		return play[1];
	}

	private double MinMax(IBoard board, ulong depth, bool isMin)
	{
		if (depth >= _maxDepth) return GetBoardScore(board);
		IncreaseFrequency(board);
		// if (CacheContains(board))
		// {
		// 	double cached = LoadFromCache(board);
		// 	if (IsTied(board) || GameEnded(board))
		// 	{
		// 		if (cached != GetBoardScore(board))
		// 		{
		// 			Console.WriteLine("Inaccuracy!");
		// 		}
		// 	}
		// 	else
		// 	{
		// 		List<IBoard> ch;
		// 		(ch, _) = board.children();
		// 		if (ch.Count > 1)
		// 		{
		// 			IncreaseFrequency(ch[0]);
		// 			double sc0 = LoadFromCache(ch[0]);
		// 			DecreaseFrequency(ch[0]);
		// 			IncreaseFrequency(ch[1]);
		// 			double sc1 = LoadFromCache(ch[1]);
		// 			DecreaseFrequency(ch[1]);
		//
		// 			if (isMin && (double.Min(sc0, sc1) != cached))
		// 			{
		// 				Console.WriteLine(board);
		// 				Console.WriteLine("Inaccuracy!");
		// 			}
		// 			else if (!isMin && (double.Max(sc0, sc1) != cached))
		// 			{
		// 				Console.WriteLine(board);
		// 				Console.WriteLine("Inaccuracy!");
		// 			}
		// 		}
		// 	}
		// 	DecreaseFrequency(board);
		// 	return cached;
		// }
		if (IsTied(board) || GameEnded(board))
		{
			double currScore = GetBoardScore(board);
			// SaveToCache(board, currScore);
			DecreaseFrequency(board);
			return currScore;
		}

		List<IBoard> children;
		(children, _) = board.children();

		double score0 = MinMax(children[0], depth + 1, !isMin);
		if (children.Count <= 1)
		{
			// SaveToCache(board, score0);
			DecreaseFrequency(board);
			return score0;
		}
		double score1 = MinMax(children[1], depth + 1, !isMin);

		double score = isMin ? double.Min(score0, score1) : double.Max(score0, score1);
		// SaveToCache(board, score);
		DecreaseFrequency(board);
		return score;
	}

	private void SaveToCache(IBoard board, double score)
	{
		int frequency = _boardFrequency[board];
		while (_cache.Count < frequency)
		{
			_cache.Add(new Dictionary<IBoard, double>());
		}
		_cache[frequency - 1].TryAdd(board, score);
	}

	private bool CacheContains(IBoard board)
	{
		int frequency = _boardFrequency[board];
		if (_cache.Count < frequency)
		{
			return false;
		}
		return _cache[frequency - 1].ContainsKey(board);
	}

	private double LoadFromCache(IBoard board)
	{
		return _cache[_boardFrequency[board] - 1][board];
	}

	private void IncreaseFrequency(IBoard board)
	{
		if (!_boardFrequency.ContainsKey(board))
		{
			_boardFrequency.Add(board, 0);
		}
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

	private double GetBoardScore(IBoard board)
	{
		return board.score(_position);
	}

	private bool GameEnded(IBoard board)
	{
		return board.winner() != -2;
	}

	private bool HasReset(IBoard board)
	{
		bool hasReset = false;
		if (_firstBoardHash == -1)
		{
			_firstBoardHash = board.hash();
			hasReset = true;
		}
		else if (_firstBoardHash == board.hash())
		{
			hasReset = true;
		}
		board.nextPlayer();
		if (_firstBoardHash == board.hash())
		{
			_firstBoardHash = board.hash();
			hasReset = true;
		}
		board.nextPlayer();
		return hasReset;
	}
}

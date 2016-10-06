public class Quad : Region
{
    public int branchSize;
    public Region[,] branches = new Region[2, 2];

    public Quad(int branchSize)
    {
        this.branchSize = branchSize;
    }

    private Region GetBranch(int x, int y)
    {
        return branches[x / branchSize, y / branchSize];
    }

    private Region NewBranch(int x, int y)
    {
        Region branch;
        if (branchSize >= 2)
        {
            branch = new Quad(branchSize / 2);
        }
        else
        {
            branch = new Tile();
        }
        branches[x / branchSize, y / branchSize] = branch;
        return branch;
    }

    public override Tile GetTile(int x, int y)
    {
        Region branch = GetBranch(x, y);
        if (branch != null)
        {
            return branch.GetTile(x % branchSize, y % branchSize);
        }
        else
        {
            return null;
        }
    }

    public override int GetHeight(int x, int y)
    {
        Region branch = GetBranch(x, y);
        if (branch != null)
        {
            return branch.GetHeight(x % branchSize, y % branchSize);
        }
        else
        {
            return 0;
        }
    }

    public override Team GetTeam(int x, int y)
    {
        Region branch = GetBranch(x, y);
        if (branch != null)
        {
            return branch.GetTeam(x % branchSize, y % branchSize);
        }
        else
        {
            return null;
        }
    }

    public override void SetTile(int x, int y, Tile tile)
    {
        Region branch = GetBranch(x, y);
        if (branch == null)
        {
            branch = NewBranch(x, y);
        }
        branch.SetTile(x % branchSize, y % branchSize, tile);
    }

    public override void SetHeight(int x, int y, int height)
    {
        Region branch = GetBranch(x, y);
        if (branch == null)
        {
            branch = NewBranch(x, y);
        }
        branch.SetHeight(x % branchSize, y % branchSize, height);
    }

    public override void SetTeam(int x, int y, Team team)
    {
        Region branch = GetBranch(x, y);
        if (branch == null)
        {
            branch = NewBranch(x, y);
        }
        branch.SetTeam(x % branchSize, y % branchSize, team);
    }
}
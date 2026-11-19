# Example tasks

## Worktrees

### Demo: Create, List, and Switch
```powershell
git worktree add -b demo/worktrees ../copilot-training-worktrees main
git worktree list
Set-Location ../copilot-training-worktrees
code .
git status -sb
```

### Cleanup

From the original repository directory, after committing or otherwise preserving any work:

```powershell
git worktree remove ../copilot-training-worktrees
# Optional: delete the local branch once it is merged.
git branch -d demo/worktrees
```

## Pseudo code
// Calculate the monthly premium from the annual premium.
// If the customer is under 25, add a 20% surcharge.
// If they have made no claims in the last 5 years, apply a 10% discount.
// Apply the discount after the surcharge.
// Divide by 12 and round to two decimal places.
// Reject negative premiums and invalid ages or claim-free years.

## Refactor
Program.cs refactor seperation of concern

## Unit test

## Code review

## Documentation
- Readme
- PR description
- Release notes
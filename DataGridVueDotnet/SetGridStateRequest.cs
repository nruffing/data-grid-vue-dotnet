namespace DataGridVueDotnet
{
  /// <summary>
  /// Request data interface sent by the data grid to save the current grid state.
  /// <see href="https://datagridvue.com/generated/classes/ServerSideStorageService.html">Server Side Storage Service</see>
  /// </summary>
  /// <typeparam name="TUserId">The type of the user identifier.</typeparam>
  /// <typeparam name="TGridId">The type of the grid identifier.</typeparam>
  public class SetGridStateRequest<TUserId, TGridId>
  {
    /// <summary>
    /// The unique identifier for the current user.
    /// </summary>
    public TUserId? UserId { get; set; }

    /// <summary>
    /// The unique identifier for the current grid.
    /// </summary>
    public TGridId? GridId { get; set; }

    /// <summary>
    /// The current grid state to save.
    /// </summary>
    public GridState? GridState { get; set; }
  }
}

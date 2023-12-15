import NavLink from "./NavLink";

const Sidebar = () => {
  return (
    <div className="sidebar">
      <div>
        {/* <h1>SAKSAK</h1>
        <h2>Menu</h2> */}
        <ul>
          <li>
            <NavLink slug={"/dashboard"}>
              <span>Dashboard</span>
            </NavLink>
            <NavLink slug={"/dashboard"}>
              <span>Customer</span>
            </NavLink>
            <NavLink slug={"/dashboard"}>
              <span>Contracts</span>
            </NavLink>
            <NavLink slug={"/dashboard"}>
              <span>Estimate</span>
            </NavLink>
            <NavLink slug={"/dashboard"}>
              <span>Task</span>
            </NavLink>
            <NavLink slug={"/dashboard"}>
              <span>Order</span>
            </NavLink>
          </li>
        </ul>
      </div>
    </div>
  );
};

export default Sidebar;

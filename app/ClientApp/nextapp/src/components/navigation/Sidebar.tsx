import NavLink from "./NavLink";

const Sidebar = () => {
  return (
    <div className="sidebar">
      <div>
        <h2>Menu</h2>

        <ul>
          <li>
            <NavLink slug={"/"}>
              <span>Dashboard</span>
            </NavLink>
          </li>
        </ul>
      </div>
    </div>
  );
};

export default Sidebar;

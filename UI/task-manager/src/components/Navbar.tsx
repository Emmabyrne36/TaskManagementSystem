import { Link } from 'react-router-dom';
import '../css/Navbar.css';

const Navbar = () => {
  return (
    <nav className="navbar">
      <div className="container">
        <h1 className="logo">📝 Task Manager</h1>
        <div className="nav-buttons">
          <Link to="/" className="nav-button">Home</Link>
          <Link to="/create" className="nav-button create-button">+ Create Task</Link>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;

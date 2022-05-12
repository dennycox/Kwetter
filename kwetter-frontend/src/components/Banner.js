import { Navbar, Nav, Container, NavDropdown, Row } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from "react";
import jwt_decode from "jwt-decode";
import { useHistory } from 'react-router-dom';

const Banner = () => {

    const [CurrentUser, setCurrentUser] = useState("");
    const [LoginStatus, setLoginStatus] = useState(false);
    const Navigate = useHistory();

    useEffect(() => {
        const token = localStorage.getItem("Token");
        token != null ? setLoginStatus(true) : setLoginStatus(false);
        if (token != null) {
          let decoded = jwt_decode(token);
          setCurrentUser(decoded.given_name);
        } else {
          setCurrentUser("");
        }
    }, []);

    const handleLogout = () => {
        localStorage.removeItem("Token");
        setLoginStatus(false);
        setCurrentUser("");
        Navigate.push("/");
    };

    return (
        <Navbar bg="light" expand="lg">
            <Container>
                <Navbar.Brand href="/home">
                    <h2>Kwetter</h2>
                </Navbar.Brand>
                <Nav className="ml-auto">
                    <Row>
                    {LoginStatus == false ? (
                    <Nav.Link href="/login" className="ml-2 mr-5">
                        Login
                    </Nav.Link>
                ) : (
                    <>
                        <NavDropdown
                        title={CurrentUser}
                        id="nav-dropdown"
                        className="p-0"
                        >
                        <NavDropdown.Item eventKey="1" href="/profile">
                            Profile
                        </NavDropdown.Item>
                        <NavDropdown.Item eventKey="2" onClick={handleLogout}>
                            Sign out
                        </NavDropdown.Item>
                        </NavDropdown>
                        </>
                        )}
                    </Row>
                </Nav>
            </Container>
        </Navbar>
    )
}

export default Banner;
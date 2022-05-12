import React, { useState } from "react";
import { Button, Container, Form, Row } from 'react-bootstrap';
import { Link, useHistory } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import AuthenticationService from "../services/AuthenticationService";

function Register() {
    const [register, setRegister] = useState({
        name: "",
        email: "",
        password: "",
        bio: "",
        location: "",
        website: "",
      });
      const [message, setMessage] = useState("");
      const [messagePassword, setMessagePassword] = useState([]);
      const [messageName, setMessagename] = useState([]);
      const [messageEmail, setMessageEmail] = useState([]);
      const [confirmPassword, setConfirmPassword] = useState("");
      const Navigate = useHistory();
    
      const handleChange = (event) => {
        setRegister({ ...register, [event.target.name]: event.target.value });
      };
    
      const handleConfirmPasswordChange = (event) => {
        setConfirmPassword(event.target.value);
      };
    
      const handleSubmit = (event) => {
        var isValid = true;
        var passwordErrorList = [];
        var nameErrorList = [];
        var emailErrorList = [];
    
        event.preventDefault();
        setMessagePassword([]);
        setMessagename([]);
        setMessageEmail([]);
        setMessage("");
    
        //check if name isn't empty
        if (register.name.length === 0) {
          nameErrorList.push("Can't be empty");
          isValid = false;
        }
    
        //check if name doesn't contain symbols
        if (!RegExp(/^[a-zA-Z0-9]+$/).test(register.name)) {
          nameErrorList.push("Can't contain any symbols");
          isValid = false;
        }
    
        //check if email isn't empty
        if (register.email.length === 0) {
          emailErrorList.push("Can't be empty");
          isValid = false;
        }
    
        //check if email contains . and @
        if (
          !RegExp(
            /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i
          ).test(register.email)
        ) {
          emailErrorList.push("Not a valid e-mail address");
          isValid = false;
        }

        //check if passwords match
        if (confirmPassword !== register.password) {
        passwordErrorList.push("Passwords don't match");
        isValid = false;
        }

        //check if password is between 8 and 200 characters
        if (
          register.password.length < 8 ||
          register.password.length > 200 ||
          register.password.length === 0
        ) {
          passwordErrorList.push("Must be 8-200 characters long");
          isValid = false;
        }
        // Check for capital letters
        if (!RegExp(/.*[A-Z]+.*/g).test(register.password)) {
          passwordErrorList.push("Must contain a capital letter");
          isValid = false;
        }
        // Check for lower letters
        if (!RegExp(/.*[a-z]+.*/g).test(register.password)) {
          passwordErrorList.push("Must contain a lower letter");
          isValid = false;
        }
        // check for numbers
        if (!RegExp(/.*[0-1-2-3-4-5-6-7-8-9]+.*/g).test(register.password)) {
          passwordErrorList.push("Must contain a number");
          isValid = false;
        }
        if (isValid === false) {
          setMessagePassword(passwordErrorList);
          setMessagename(nameErrorList);
          setMessageEmail(emailErrorList);
          return;
        }
    
        AuthenticationService.register(register)
          .then((res) => {
            setMessage("Account created");
            Navigate.push("/login");
          })
          .catch((error) => {
            setMessage("Something went wrong, try again in a few minutes ...");
          });
      };

    return (
        <Container className="pt-3">
            <h3>Create account</h3>
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <Form.Label>Name</Form.Label>
                    <Form.Control type="text" onChange={handleChange} name="name" required />
                </Form.Group>
                <div>
                    {messageName.map((message, index) => (
                      <div key={index}>
                        <small className="help-block text-danger">
                          {message}
                        </small>{" "}
                        <br></br>
                      </div>
                    ))}
                  </div>
                <Form.Group>
                    <Form.Label>E-mail address</Form.Label>
                    <Form.Control type="email" onChange={handleChange} name="email" required />
                </Form.Group>
                <div>
                    {messageEmail.map((message, index) => (
                      <div key={index}>
                        <small className="help-block text-danger">
                          {message}
                        </small>{" "}
                        <br></br>
                      </div>
                    ))}
                  </div>
                <Form.Group>
                    <Form.Label>Bio</Form.Label>
                    <Form.Control as="textarea" rows={3} onChange={handleChange} name="bio" />                
                </Form.Group>
                <Form.Group>
                    <Form.Label>Location</Form.Label>
                    <Form.Control type="text" onChange={handleChange} name="location" />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Website</Form.Label>
                    <Form.Control type="text" onChange={handleChange} name="website" />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" onChange={handleChange} name="password" required />
                </Form.Group>
                <div>
                    {messagePassword.map((message, index) => (
                      <div key={index}>
                        <small className="help-block text-danger">
                          {message}
                        </small>{" "}
                        <br></br>
                      </div>
                    ))}
                  </div>
                <Form.Group>
                    <Form.Label>Confirm password</Form.Label>
                    <Form.Control type="password" onChange={handleConfirmPasswordChange} name="confirmPassword" required />
                </Form.Group>
                <Form.Group>
                    <Button type="submit">Create account</Button>
                </Form.Group>
                <Link to={"/login"}>Already have an account? Login here</Link>
            </Form>
            <Row>
                <p>{message}</p>    
            </Row>
        </Container>
    )
}

export default Register;
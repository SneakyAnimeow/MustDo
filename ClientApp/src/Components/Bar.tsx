import { Navbar, Container } from "react-bootstrap";

export const Bar = () => {
  return (
    <>
      <Navbar bg="primary" variant="dark">
        <Container>
          <Navbar.Brand>
            <img
              alt=""
              src="https://cdn.iconscout.com/icon/free/png-256/notepad-2642816-2192663.png"
              width="30"
              height="30"
              className="d-inline-block align-top"
            />{" "}
            MustDo
          </Navbar.Brand>
        </Container>
      </Navbar>
    </>
  );
};

export default Bar;

import { useEffect, useState } from "react"
import { Col, Container, Row, Card, CardHeader, CardBody, Button } from "reactstrap"
import TablaLibros from "./componentes/TablaLibros"
import ModalLibros from "./componentes/ModalLibros"

const App = () => {
    const [libros, setLibros] = useState([]);
    const [mostrarModal, setMostrarModal] = useState(false);
    const [editar, setEditar] = useState(null)

    const mostrarLibros = async () => {
        const response = await fetch("https://localhost:7103/api/Libros/Lista");
        if (response.ok) {
            const data = await response.json();
            setLibros(data);
        } else {
            console.log("error en lista");
        }
    };

    useEffect(() => {
        mostrarLibros()
    }, [])

    const guardarLibro = async (libro) => {
        const response = await fetch("https://localhost:7103/api/Libros/", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(libro)
        })

        if (response.ok) {
            setMostrarModal(false);
            mostrarLibros();
        }
    }


    const editarLibro = async (libro) => {

        const response = await fetch("https://localhost:7103/api/Libros/Editar", {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(libro)
        })

        if (response.ok) {
            setMostrarModal(!mostrarModal);
            mostrarLibros();
        }
    }



    const eliminarLibro = async (id) => {

        const respuesta = window.confirm("Desea eliminar este Registro?")

        if (!respuesta) {
            return;
        }
        const response = await fetch(`https://localhost:7103/api/Libros/${id}`, {
            method: 'DELETE',
        });
        if (response.ok) {
            mostrarLibros();
        }
    };


    return (
        <Container>
            <Row className="mt-5">
                <Col sm="14">
                    <Card>
                        <CardHeader>
                            <h4>SISTEMA BLIBLIOTECA</h4>
                        </CardHeader>
                        <CardBody>
                            <Button size="10" color="warning"
                                onClick={() => setMostrarModal(!mostrarModal)}
                            >Agregar Nuevo Libro +</Button>
                            <hr></hr>
                            <TablaLibros
                                data={libros}
                                setEditar={setEditar}
                                mostrarModal={mostrarModal}
                                setMostrarModal={setMostrarModal}
                                eliminarLibro={eliminarLibro}
                            />
                        </CardBody>
                    </Card>
                </Col>
            </Row>
            <ModalLibros
                mostrarModal={mostrarModal}
                setMostrarModal={setMostrarModal}
                guardarLibro={guardarLibro}
                editar={editar}
                setEditar={setEditar}
                editarLibro={editarLibro}
            />
        </Container>
    )
}

export default App;

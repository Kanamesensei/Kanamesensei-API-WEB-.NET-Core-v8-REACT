import { useEffect, useState } from "react";
import { Modal, ModalBody, ModalHeader, Form, FormGroup, Input, Label, ModalFooter, Button } from "reactstrap";

const modeloLibros = {
    libroID: 0,
    nombre: "",
    genero: "",
    descripcion: "",
    estatus: ""
}

const ModalLibros = ({ mostrarModal, setMostrarModal, guardarLibro, editar, setEditar, editarLibro }) => {

    const [libro, setLibro] = useState(modeloLibros);
    const [validacionRealizada, setValidacionRealizada] = useState(false);

    const actualizarDato = (e) => {

        console.log(e.target.name + " : " + e.target.value)
        setLibro(
            {
                ...libro,
                [e.target.name]: e.target.value
            }
        )
    }


    const enviarDatos = () => {

        if (!validacionRealizada && Object.values(libro).some(value => typeof value !== 'string' || value.trim() === "")) {
            alert("Por favor, complete todos los campos.");
            setValidacionRealizada(true);
            return;
        }
        if (libro.libroID === 0) {
            guardarLibro(libro);
        } else {
            editarLibro(libro);
        }
        setLibro(modeloLibros);
        setValidacionRealizada(false);
    }




    useEffect(() => {
        if (editar != null) {
            setLibro(editar)
        } else {
            setLibro(modeloLibros)
        }
    }, [editar])


    const cerrarModal = () => {

        setMostrarModal(!mostrarModal)
        setEditar(null)
    }

    return (

        <Modal isOpen={mostrarModal}>
            <ModalHeader>

                {libro.libroID == 0 ? "Nuevo Libro" : "Editar Libro"}

            </ModalHeader>
            <ModalBody>
                <Form>
                    <FormGroup>
                        <Label>Nombre</Label>
                        <Input name="nombre" onChange={(e) => actualizarDato(e)} value={libro.nombre} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Genero</Label>
                        <Input name="genero" onChange={(e) => actualizarDato(e)} value={libro.genero} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Descripcion</Label>
                        <Input name="descripcion" onChange={(e) => actualizarDato(e)} value={libro.descripcion} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Estatus</Label>
                        <Input name="estatus" onChange={(e) => actualizarDato(e)} value={libro.estatus} />
                    </FormGroup>
                </Form>
            </ModalBody>


            <ModalFooter>
                <Button color="primary" size="sm" onClick={enviarDatos}>Guardar</Button>
                <Button color="danger" size="sm" onClick={cerrarModal} >Cerrar</Button>
            </ModalFooter>
        </Modal>
    )
}
export default ModalLibros;

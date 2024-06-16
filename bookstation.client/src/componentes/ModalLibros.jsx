import React, { useEffect, useState } from "react";
import { Modal, ModalBody, ModalHeader, Form, FormGroup, Input, Label, ModalFooter, Button } from "reactstrap";

const modeloLibros = {
    libroID: 0,
    nombre: "",
    genero: "",
    descripcion: "",
    estatus: ""
};

const ModalLibros = ({ mostrarModal, setMostrarModal, guardarLibro, editar, setEditar, editarLibro }) => {
    const [libro, setLibro] = useState(modeloLibros);
    const [validacionRealizada, setValidacionRealizada] = useState(false);

    const actualizarDato = (e) => {
        const { name, value } = e.target;
        setLibro(prevLibro => ({
            ...prevLibro,
            [name]: value
        }));
    };


    const enviarDatos = async () => {
        try {

            if (!validacionRealizada && Object.values(libro).some(value => typeof value !== 'string' || value.trim() === "")) {
                alert("Por favor, complete todos los campos correctamente o Edite los existentes");
                setValidacionRealizada(true);
                return;
            }


            if (libro.libroID === 0) {
                await guardarLibro(libro);
            } else {
                await editarLibro(libro);
            }

            setLibro(modeloLibros);
            setValidacionRealizada(false);
            setMostrarModal(false);
            setEditar(null);
        } catch (error) {
            console.error("Error al guardar/editar el libro:", error);

        }
    };


    useEffect(() => {
        if (editar) {
            setLibro(editar);
        } else {
            setLibro(modeloLibros);
        }
    }, [editar]);


    const cerrarModal = () => {
        setMostrarModal(false);
        setEditar(null);
    };

    return (
        <Modal isOpen={mostrarModal}>
            <ModalHeader>
                {libro.libroID === 0 ? "Nuevo Libro" : "Editar Libro"}
            </ModalHeader>
            <ModalBody>
                <Form>
                    <FormGroup>
                        <Label>Nombre</Label>
                        <Input name="nombre" onChange={actualizarDato} value={libro.nombre} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Genero</Label>
                        <Input name="genero" onChange={actualizarDato} value={libro.genero} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Descripcion</Label>
                        <Input name="descripcion" onChange={actualizarDato} value={libro.descripcion} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Estatus</Label>
                        <Input name="estatus" onChange={actualizarDato} value={libro.estatus} />
                    </FormGroup>
                </Form>
            </ModalBody>
            <ModalFooter>
                <Button color="primary" size="sm" onClick={enviarDatos}>Guardar</Button>
                <Button color="danger" size="sm" onClick={cerrarModal}>Cerrar</Button>
            </ModalFooter>
        </Modal>
    );
};

export default ModalLibros;

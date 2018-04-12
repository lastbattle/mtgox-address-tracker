using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MJB_whale_whisperer.Utils
{
    public class ConsoleOutputFileStream : TextWriter
    {
        // Console writer
        private TextWriter DefaultConsoleWriter;

        // File Writer
        private FileStream fs;
        private StreamWriter sw;

        public ConsoleOutputFileStream(TextWriter DefaultConsoleWriter, string OutputFileName)
        {
            this.DefaultConsoleWriter = DefaultConsoleWriter;

            CreateFileStream(OutputFileName);
        }

        /// <summary>
        /// Creates the file stream to write to file
        /// </summary>
        /// <param name="OutputFileName"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateFileStream(string OutputFileName)
        {
            fs = new FileStream(OutputFileName, FileMode.OpenOrCreate);
            sw = new StreamWriter(fs);
            sw.AutoFlush = true;
        }

        public override Encoding Encoding => Encoding.UTF8;

        //
        // Summary:
        //     Closes the current writer and releases any system resources associated with the
        //     writer.
        public override void Close()
        {
            fs.Close();
            DefaultConsoleWriter.Close();
        }

        //
        // Summary:
        //     Clears all buffers for the current writer and causes any buffered data to be
        //     written to the underlying device.
        public override void Flush()
        {
            fs.Close();
            DefaultConsoleWriter.Flush();
        }
        //
        // Summary:
        //     Asynchronously clears all buffers for the current writer and causes any buffered
        //     data to be written to the underlying device.
        //
        // Returns:
        //     A task that represents the asynchronous flush operation.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The text writer is disposed.
        //
        //   T:System.InvalidOperationException:
        //     The writer is currently in use by a previous write operation.
        public override Task FlushAsync()
        {
            fs.FlushAsync();
            return DefaultConsoleWriter.FlushAsync();
        }
        //
        // Summary:
        //     Writes the text representation of an 8-byte unsigned integer to the text string
        //     or stream.
        //
        // Parameters:
        //   value:
        //     The 8-byte unsigned integer to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(ulong value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes the text representation of a 4-byte unsigned integer to the text string
        //     or stream.
        //
        // Parameters:
        //   value:
        //     The 4-byte unsigned integer to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(uint value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes a formatted string to the text string or stream, using the same semantics
        //     as the System.String.Format(System.String,System.Object[]) method.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg:
        //     An object array that contains zero or more objects to format and write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format or arg is null.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        //
        //   T:System.FormatException:
        //     format is not a valid composite format string. -or- The index of a format item
        //     is less than 0 (zero), or greater than or equal to the length of the arg array.
        public override void Write(string format, params object[] arg)
        {
            sw.Write(format, arg);
            DefaultConsoleWriter.Write(format, arg);
        }
        //
        // Summary:
        //     Writes a formatted string to the text string or stream, using the same semantics
        //     as the System.String.Format(System.String,System.Object,System.Object,System.Object)
        //     method.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The first object to format and write.
        //
        //   arg1:
        //     The second object to format and write.
        //
        //   arg2:
        //     The third object to format and write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        //
        //   T:System.FormatException:
        //     format is not a valid composite format string. -or- The index of a format item
        //     is less than 0 (zero), or greater than or equal to the number of objects to be
        //     formatted (which, for this method overload, is three).
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            sw.Write(format, arg0, arg1, arg2);
            DefaultConsoleWriter.Write(format, arg0, arg1, arg2);
        }
        //
        // Summary:
        //     Writes a formatted string to the text string or stream, using the same semantics
        //     as the System.String.Format(System.String,System.Object,System.Object) method.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The first object to format and write.
        //
        //   arg1:
        //     The second object to format and write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        //
        //   T:System.FormatException:
        //     format is not a valid composite format string. -or- The index of a format item
        //     is less than 0 (zero) or greater than or equal to the number of objects to be
        //     formatted (which, for this method overload, is two).
        public override void Write(string format, object arg0, object arg1)
        {
            sw.Write(format, arg0, arg1);
            DefaultConsoleWriter.Write(format, arg0, arg1);
        }
        //
        // Summary:
        //     Writes a formatted string to the text string or stream, using the same semantics
        //     as the System.String.Format(System.String,System.Object) method.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The object to format and write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        //
        //   T:System.FormatException:
        //     format is not a valid composite format string. -or- The index of a format item
        //     is less than 0 (zero), or greater than or equal to the number of objects to be
        //     formatted (which, for this method overload, is one).
        public override void Write(string format, object arg0)
        {
            sw.Write(format, arg0);
            DefaultConsoleWriter.Write(format, arg0);
        }
        //
        // Summary:
        //     Writes a string to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The string to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(string value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes the text representation of an object to the text string or stream by calling
        //     the ToString method on that object.
        //
        // Parameters:
        //   value:
        //     The object to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(object value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes the text representation of an 8-byte signed integer to the text string
        //     or stream.
        //
        // Parameters:
        //   value:
        //     The 8-byte signed integer to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(long value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes the text representation of a 4-byte signed integer to the text string
        //     or stream.
        //
        // Parameters:
        //   value:
        //     The 4-byte signed integer to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(int value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes the text representation of an 8-byte floating-point value to the text
        //     string or stream.
        //
        // Parameters:
        //   value:
        //     The 8-byte floating-point value to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(double value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes the text representation of a decimal value to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The decimal value to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(decimal value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes a subarray of characters to the text string or stream.
        //
        // Parameters:
        //   buffer:
        //     The character array to write data from.
        //
        //   index:
        //     The character position in the buffer at which to start retrieving data.
        //
        //   count:
        //     The number of characters to write.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The buffer length minus index is less than count.
        //
        //   T:System.ArgumentNullException:
        //     The buffer parameter is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index or count is negative.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(char[] buffer, int index, int count)
        {
            sw.Write(buffer, index, count);
            DefaultConsoleWriter.Write(buffer, index, count);
        }
        //
        // Summary:
        //     Writes a character array to the text string or stream.
        //
        // Parameters:
        //   buffer:
        //     The character array to write to the text stream.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(char[] buffer)
        {
            sw.Write(buffer);
            DefaultConsoleWriter.Write(buffer);
        }
        //
        // Summary:
        //     Writes a character to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The character to write to the text stream.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(char value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes the text representation of a Boolean value to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The Boolean value to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(bool value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes the text representation of a 4-byte floating-point value to the text string
        //     or stream.
        //
        // Parameters:
        //   value:
        //     The 4-byte floating-point value to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(float value)
        {
            sw.Write(value);
            DefaultConsoleWriter.Write(value);
        }
        //
        // Summary:
        //     Writes a string to the text string or stream asynchronously.
        //
        // Parameters:
        //   value:
        //     The string to write. If value is null, nothing is written to the text stream.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The text writer is disposed.
        //
        //   T:System.InvalidOperationException:
        //     The text writer is currently in use by a previous write operation.
        public override Task WriteAsync(string value)
        {
            sw.WriteAsync(value);
            return DefaultConsoleWriter.WriteAsync(value);
        }

        //
        // Summary:
        //     Writes a character to the text string or stream asynchronously.
        //
        // Parameters:
        //   value:
        //     The character to write to the text stream.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The text writer is disposed.
        //
        //   T:System.InvalidOperationException:
        //     The text writer is currently in use by a previous write operation.
        public override Task WriteAsync(char value)
        {
            sw.WriteAsync(value);
            return DefaultConsoleWriter.WriteAsync(value);
        }
        //
        // Summary:
        //     Writes a subarray of characters to the text string or stream asynchronously.
        //
        // Parameters:
        //   buffer:
        //     The character array to write data from.
        //
        //   index:
        //     The character position in the buffer at which to start retrieving data.
        //
        //   count:
        //     The number of characters to write.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     buffer is null.
        //
        //   T:System.ArgumentException:
        //     The index plus count is greater than the buffer length.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index or count is negative.
        //
        //   T:System.ObjectDisposedException:
        //     The text writer is disposed.
        //
        //   T:System.InvalidOperationException:
        //     The text writer is currently in use by a previous write operation.
        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            sw.WriteAsync(buffer, index, count);
            return DefaultConsoleWriter.WriteAsync(buffer, index, count);
        }
        //
        // Summary:
        //     Writes a formatted string and a new line to the text string or stream, using
        //     the same semantics as the System.String.Format(System.String,System.Object) method.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The object to format and write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        //
        //   T:System.FormatException:
        //     format is not a valid composite format string. -or- The index of a format item
        //     is less than 0 (zero), or greater than or equal to the number of objects to be
        //     formatted (which, for this method overload, is one).
        public override void WriteLine(string format, object arg0)
        {
            sw.WriteLine(format, arg0);
            DefaultConsoleWriter.WriteLine(format, arg0);
        }
        //
        // Summary:
        //     Writes the text representation of an 8-byte unsigned integer followed by a line
        //     terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The 8-byte unsigned integer to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(ulong value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes the text representation of a 4-byte unsigned integer followed by a line
        //     terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The 4-byte unsigned integer to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(uint value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes out a formatted string and a new line, using the same semantics as System.String.Format(System.String,System.Object).
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg:
        //     An object array that contains zero or more objects to format and write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     A string or object is passed in as null.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        //
        //   T:System.FormatException:
        //     format is not a valid composite format string. -or- The index of a format item
        //     is less than 0 (zero), or greater than or equal to the length of the arg array.
        public override void WriteLine(string format, params object[] arg)
        {
            sw.WriteLine(format, arg);
            DefaultConsoleWriter.WriteLine(format, arg);
        }
        //
        // Summary:
        //     Writes out a formatted string and a new line, using the same semantics as System.String.Format(System.String,System.Object).
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The first object to format and write.
        //
        //   arg1:
        //     The second object to format and write.
        //
        //   arg2:
        //     The third object to format and write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        //
        //   T:System.FormatException:
        //     format is not a valid composite format string. -or- The index of a format item
        //     is less than 0 (zero), or greater than or equal to the number of objects to be
        //     formatted (which, for this method overload, is three).
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            sw.WriteLine(format, arg0, arg1, arg2);
            DefaultConsoleWriter.WriteLine(format, arg0, arg1, arg2);
        }
        //
        // Summary:
        //     Writes a formatted string and a new line to the text string or stream, using
        //     the same semantics as the System.String.Format(System.String,System.Object,System.Object)
        //     method.
        //
        // Parameters:
        //   format:
        //     A composite format string.
        //
        //   arg0:
        //     The first object to format and write.
        //
        //   arg1:
        //     The second object to format and write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     format is null.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        //
        //   T:System.FormatException:
        //     format is not a valid composite format string. -or- The index of a format item
        //     is less than 0 (zero), or greater than or equal to the number of objects to be
        //     formatted (which, for this method overload, is two).
        public override void WriteLine(string format, object arg0, object arg1)
        {
            sw.WriteLine(format, arg0, arg1);
            DefaultConsoleWriter.WriteLine(format, arg0, arg1);
        }
        //
        // Summary:
        //     Writes a string followed by a line terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The string to write. If value is null, only the line terminator is written.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(string value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes the text representation of a 4-byte floating-point value followed by a
        //     line terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The 4-byte floating-point value to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(float value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes a line terminator to the text string or stream.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine()
        {
            sw.WriteLine();
            DefaultConsoleWriter.WriteLine();
        }
        //
        // Summary:
        //     Writes the text representation of an 8-byte signed integer followed by a line
        //     terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The 8-byte signed integer to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(long value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes the text representation of a 4-byte signed integer followed by a line
        //     terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The 4-byte signed integer to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(int value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes the text representation of a 8-byte floating-point value followed by a
        //     line terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The 8-byte floating-point value to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(double value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes the text representation of a decimal value followed by a line terminator
        //     to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The decimal value to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(decimal value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes a subarray of characters followed by a line terminator to the text string
        //     or stream.
        //
        // Parameters:
        //   buffer:
        //     The character array from which data is read.
        //
        //   index:
        //     The character position in buffer at which to start reading data.
        //
        //   count:
        //     The maximum number of characters to write.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The buffer length minus index is less than count.
        //
        //   T:System.ArgumentNullException:
        //     The buffer parameter is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index or count is negative.
        //
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(char[] buffer, int index, int count)
        {
            sw.WriteLine(buffer, index, count);
            DefaultConsoleWriter.WriteLine(buffer, index, count);
        }
        //
        // Summary:
        //     Writes an array of characters followed by a line terminator to the text string
        //     or stream.
        //
        // Parameters:
        //   buffer:
        //     The character array from which data is read.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(char[] buffer)
        {
            sw.WriteLine(buffer);
            DefaultConsoleWriter.WriteLine(buffer);
        }
        //
        // Summary:
        //     Writes a character followed by a line terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The character to write to the text stream.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(char value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes the text representation of a Boolean value followed by a line terminator
        //     to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The Boolean value to write.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(bool value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes the text representation of an object by calling the ToString method on
        //     that object, followed by a line terminator to the text string or stream.
        //
        // Parameters:
        //   value:
        //     The object to write. If value is null, only the line terminator is written.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The System.IO.TextWriter is closed.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurs.
        public override void WriteLine(object value)
        {
            sw.WriteLine(value);
            DefaultConsoleWriter.WriteLine(value);
        }
        //
        // Summary:
        //     Writes a line terminator asynchronously to the text string or stream.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The text writer is disposed.
        //
        //   T:System.InvalidOperationException:
        //     The text writer is currently in use by a previous write operation.
        public override Task WriteLineAsync()
        {
            sw.WriteLineAsync();
            return DefaultConsoleWriter.WriteLineAsync();
        }
        //
        // Summary:
        //     Writes a character followed by a line terminator asynchronously to the text string
        //     or stream.
        //
        // Parameters:
        //   value:
        //     The character to write to the text stream.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The text writer is disposed.
        //
        //   T:System.InvalidOperationException:
        //     The text writer is currently in use by a previous write operation.
        public override Task WriteLineAsync(char value)
        {
            sw.WriteLineAsync(value);
            return DefaultConsoleWriter.WriteLineAsync(value);
        }

        //
        // Summary:
        //     Writes a subarray of characters followed by a line terminator asynchronously
        //     to the text string or stream.
        //
        // Parameters:
        //   buffer:
        //     The character array to write data from.
        //
        //   index:
        //     The character position in the buffer at which to start retrieving data.
        //
        //   count:
        //     The number of characters to write.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     buffer is null.
        //
        //   T:System.ArgumentException:
        //     The index plus count is greater than the buffer length.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index or count is negative.
        //
        //   T:System.ObjectDisposedException:
        //     The text writer is disposed.
        //
        //   T:System.InvalidOperationException:
        //     The text writer is currently in use by a previous write operation.
        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            sw.WriteLineAsync(buffer, index, count);
            return DefaultConsoleWriter.WriteLineAsync(buffer, index, count);
        }
        //
        // Summary:
        //     Writes a string followed by a line terminator asynchronously to the text string
        //     or stream.
        //
        // Parameters:
        //   value:
        //     The string to write. If the value is null, only a line terminator is written.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The text writer is disposed.
        //
        //   T:System.InvalidOperationException:
        //     The text writer is currently in use by a previous write operation.
        public override Task WriteLineAsync(string value)
        {
            sw.WriteLineAsync(value);
            return DefaultConsoleWriter.WriteLineAsync(value);
        }
        //
        // Summary:
        //     Releases the unmanaged resources used by the System.IO.TextWriter and optionally
        //     releases the managed resources.
        //
        // Parameters:
        //   disposing:
        //     true to release both managed and unmanaged resources; false to release only unmanaged
        //     resources.
        protected override void Dispose(bool disposing)
        {
            sw.Dispose();
            fs.Dispose();
            DefaultConsoleWriter.Dispose();
        }
    }
}
